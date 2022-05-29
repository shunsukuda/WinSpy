using System;
using DllImport;

namespace WinSpy
{
    public partial class Form1 : Form
    {
        private Win32Api.POINT pos;
        private IntPtr hwnd;

        public Form1()
        {
            InitializeComponent();
        }

        private string getPos()
        {
            Win32Api.GetCursorPos(out this.pos);
            return string.Format("pos=({0},{1})", this.pos.X, this.pos.Y);
        }

        private string getHandler(ref IntPtr hwnd)
        {
            hwnd = Win32Api.WindowFromPoint(this.pos);
            return string.Format("handler={0}", hwnd);
        }

        private string getClassName(IntPtr hwnd)
        {
            return string.Format("ClassName={0}", Win32Funcs.GetClassName(hwnd, 256));
        }

        private string getWindowText(IntPtr hwnd)
        {
            return string.Format("WindowText={0}", Win32Funcs.GetWindowText(hwnd));
        }

        private string getParents(IntPtr hwnd)
        {
            string s = "";
            List<IntPtr> list = Win32Funcs.GetWindowParents(hwnd);
            foreach (IntPtr p in list) s += "/" + p.ToString(); 
            return string.Format("Parents={0}", s);
        }

        private string getChildren(IntPtr hwnd)
        {
            string s = "";
            List<IntPtr> list = Win32Funcs.GetWindowChildren(hwnd, IntPtr.Zero);
            foreach (IntPtr p in list) s +=  p.ToString() + " ";
            return string.Format("Children={0}", s);
        }

        private string getWindowRect(IntPtr hwnd)
        {
            Win32Api.RECT rc = new Win32Api.RECT();
            Win32Api.GetWindowRect(hwnd, out rc);
            return string.Format("WindowRect={0}", rc.ToString());
        }

        private string getClientRect(IntPtr hwnd)
        {
            Win32Api.RECT rc = new Win32Api.RECT();
            Win32Api.GetClientRect(hwnd, out rc);
            return string.Format("ClientRect={0}", rc.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.PosLabel.Text = getPos();
            this.HandlerLabel.Text = getHandler(ref this.hwnd);
            this.ClassNameLabel.Text = getClassName(this.hwnd);
            this.WindowTextLabel.Text = getWindowText(this.hwnd);
            this.ParentsLabel.Text = getParents(this.hwnd);
            this.ChildrenLabel.Text = getChildren(this.hwnd);
            this.WindowRectLabel.Text = getWindowRect(this.hwnd);
            this.ClientRectLabel.Text = getClientRect(this.hwnd);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pos = new Win32Api.POINT();
            this.timer1.Start();
        }
    }
}