using System;
using DllImport;

namespace WinSpy
{
    public partial class Form1 : Form
    {
        private Win32Api.POINT pos;
        private IntPtr hwnd;
        private Win32Api.RECT rect;
        private List<IntPtr> parents;
        private List<IntPtr> children;

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

        private string getParents(IntPtr hwnd, ref List<IntPtr> parents)
        {
            string s = "";
            parents = Win32Funcs.GetWindowParents(hwnd);
            foreach (IntPtr p in parents) s += "/" + p.ToString(); 
            return string.Format("Parents={0}", s);
        }

        private string getChildren(IntPtr hwnd, ref List<IntPtr> children)
        {
            string s = "";
            children = Win32Funcs.GetWindowChildren(hwnd, IntPtr.Zero);
            foreach (IntPtr p in children) s +=  p.ToString() + " ";
            return string.Format("Children={0}", s);
        }

        private string getWindowRect(IntPtr hwnd, ref Win32Api.RECT rect)
        {
            rect = new Win32Api.RECT();
            Win32Api.GetWindowRect(hwnd, out rect);
            return string.Format("WindowRect={0}", rect.ToString());
        }

        private string getClientRect(IntPtr hwnd)
        {
            Win32Api.RECT rc = new Win32Api.RECT();
            Win32Api.GetClientRect(hwnd, out rc);
            return string.Format("ClientRect={0}", rc.ToString());
        }

        private string getModuleFileName(IntPtr hwnd)
        {
            return string.Format("ModuleFileName={0}", Win32Funcs.GetWindowModuleFileName(hwnd,256));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.PosLabel.Text = getPos();
            this.HandlerLabel.Text = getHandler(ref this.hwnd);
            this.ClassNameLabel.Text = getClassName(this.hwnd);
            this.WindowTextLabel.Text = getWindowText(this.hwnd);
            this.ParentsLabel.Text = getParents(this.hwnd, ref this.parents);
            this.ChildrenLabel.Text = getChildren(this.hwnd, ref this.children);
            this.WindowRectLabel.Text = getWindowRect(this.hwnd, ref this.rect);
            this.ClientRectLabel.Text = getClientRect(this.hwnd);
            this.ModuleFileNameLabel.Text = getModuleFileName(this.hwnd);
            this.textBox1.Text = "";
            if (this.parents.Count != 0)
            {
                foreach (IntPtr c in Win32Funcs.GetWindowChildren(this.parents[0], IntPtr.Zero))
                {
                    IntPtr chwnd = Win32Funcs.FindWindowChildFromPoint(this.parents[0], IntPtr.Zero, this.pos);
                    this.textBox1.Text += string.Format("{0} ", c == chwnd);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pos = new Win32Api.POINT();
            this.timer1.Start();
        }
    }
}