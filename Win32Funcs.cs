using System;
using System.Text;

namespace DllImport
{
    public class Win32Funcs
    {
        private static long qpf = 0;

        public static IntPtr FindWindowChildFromPoint(IntPtr hWnd, IntPtr hWndChildAfter, Win32Api.POINT p)
        {
            List<IntPtr> list = Win32Funcs.GetWindowChildren(hWnd, hWndChildAfter);
            foreach (IntPtr c in list)
            {
                Win32Api.RECT rc = new Win32Api.RECT();
                Win32Api.GetWindowRect(c, out rc);
                if (rc.Left <= p.X && p.X <= rc.Right && rc.Top <= p.Y && p.Y <= rc.Bottom)
                {
                    return c;
                }
            }
            return IntPtr.Zero;
        }

        public static IntPtr FindWindowChildFromClassNameContains(IntPtr hWnd, IntPtr hWndChildAfter, string className)
        {
            List<IntPtr> list = Win32Funcs.GetWindowChildren(hWnd, hWndChildAfter);
            foreach (IntPtr c in list)
            {
                if (Win32Funcs.GetClassName(c, 256).Contains(className))
                {
                    return c;
                }

            }
            return IntPtr.Zero;
        }

        public static string GetClassName(IntPtr hWnd, int nMaxCount)
        {
            StringBuilder sb = new StringBuilder(nMaxCount);
            Win32Api.GetClassName(hWnd, sb, nMaxCount);
            return sb.ToString();
        }

        public static double GetQueryPerformanceSeconds()
        {
            if (qpf == 0) Win32Api.QueryPerformanceFrequency(out qpf);
            long qpc = 0;
            Win32Api.QueryPerformanceCounter(out qpc);
            return qpc / qpf;
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            int n = Win32Api.GetWindowTextLength(hWnd) + 1;
            StringBuilder sb = new StringBuilder(n);
            Win32Api.GetWindowText(hWnd, sb, n);
            return sb.ToString();
        }

        public static List<IntPtr> GetWindowChildren(IntPtr hWnd, IntPtr hWndChildAfter)
        {
            List<IntPtr> list = new List<IntPtr>();
            IntPtr chWnd = hWndChildAfter;
            do
            {
                chWnd = Win32Api.FindWindowExA(hWnd, chWnd, null, null);
                if (chWnd != IntPtr.Zero) list.Add(chWnd);
            } while (chWnd != IntPtr.Zero);
            return list;
        }

        public static List<IntPtr> GetWindowParents(IntPtr hWnd)
        {
            List<IntPtr> list = new List<IntPtr>();
            while (hWnd != IntPtr.Zero)
            {
                hWnd = Win32Api.GetParent(hWnd);
                if (hWnd != IntPtr.Zero) list.Add(hWnd);
            }
            list.Reverse();
            return list;
        }
    }
}
