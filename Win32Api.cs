using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DllImport;

public class Win32Api
{
    // Win32 Structs
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator System.Drawing.Point(POINT p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        public static implicit operator POINT(System.Drawing.Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }


    // Win32 Enums
    public enum GW : uint
    {
        /// <summary>
        /// The retrieved handle identifies the window of the same type that is highest in the Z order.
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        HWNDFIRST = 0,
        /// <summary>
        /// The retrieved handle identifies the window of the same type that is lowest in the Z order.
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        HWNDLAST = 1,
        /// <summary>
        /// The retrieved handle identifies the window below the specified window in the Z order.
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        HWNDNEXT = 2,
        /// <summary>
        /// The retrieved handle identifies the window above the specified window in the Z order.
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        HWNDPREV = 3,
        /// <summary>
        /// The retrieved handle identifies the specified window's owner window, if any.
        /// For more information, see Owned Windows(https://docs.microsoft.com/en-us/windows/win32/winmsg/window-features).
        /// </summary>
        OWNER = 4,
        /// <summary>
        /// The retrieved handle identifies the child window at the top of the Z order.
        /// If the specified window is a parent window; otherwise, the retrieved handle is NULL.
        /// The function examines only child windows of the specified window. It does not examine descendant windows.
        /// </summary>
        CHILD = 5,
        /// <summary>
        /// </summary>
        ENABLEDPOPUP = 6,
        /// <summary>
        /// The retrieved handle identifies the enabled popup window owned by the specified window
        /// (the search uses the first such window found using GW_HWNDNEXT); 
        /// otherwise, if there are no enabled popup windows, the retrieved handle is that of the specified window.
        /// </summary>
    }

    public enum HWND :int
    {
        /// <summary>
        /// Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, 
        /// the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        BOTTOM = 1,
        /// <summary>
        /// Places the window above all non-topmost windows (that is, behind all topmost windows). 
        /// This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        NOTOPMOST = -2,
        /// <summary>
        /// Places the window at the top of the Z order.
        /// </summary>
        TOP = 0,
        /// <summary>
        /// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        TOPMOST = -1,
    }


    /// <summary>
    /// SetWindowPos
    /// </summary>
    [Flags]
    public enum SWP : uint
    {
        /// <summary>
        /// If the calling thread and the thread that owns the window are attached to different input queues, 
        /// the system posts the request to the thread that owns the window. 
        /// This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        ASYNCWINDOWPOS = 0x4000,
        /// <summary>
        /// Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        DEFERERASE = 0x2000,
        /// <summary>
        /// Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        DRAWFRAME = 0x0020,
        /// <summary>
        /// Applies new frame styles set using the SetWindowLong function. 
        /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. 
        /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        FRAMECHANGED = 0x0020,
        /// <summary>
        /// Hides the window.
        /// </summary>
        HIDEWINDOW = 0x0080,
        /// <summary>
        /// Does not activate the window. If this flag is not set,the window is activated and moved to the top of either the topmost or non-topmost group 
        /// (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        NOACTIVATE = 0x0010,
        /// <summary>
        /// Discards the entire contents of the client area. 
        /// If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        NOCOPYBITS = 0x0100,
        /// <summary>
        /// Retains the current position (ignores X and Y parameters).
        /// </summary>
        NOMOVE = 0x0002,
        /// <summary>
        /// Does not change the owner window's position in the Z order.
        /// </summary>
        NOOWNERZORDER = 0x0200,
        /// <summary>
        /// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area 
        /// (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. 
        /// When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        NOREDRAW = 0x0008,
        /// <summary>
        /// Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        NOREPOSITION = 0x0200,
        /// <summary>
        /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        NOSENDCHANGING = 0x0400,
        /// <summary>
        /// Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        NOSIZE = 0x0001,
        /// <summary>
        /// Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        NOZORDER = 0x0004,
        /// <summary>
        /// Displays the window.
        /// </summary>
        SHOWWINDOW = 0x0040,
    }

    public enum KEYEVENTF : uint
    {
        /// <summary>
        /// If specified, the scan code was preceded by a prefix byte having the value 0xE0 (224).
        /// </summary>
        EXTENDEDKEY = 0x0001,
        /// <summary>
        /// If specified, the key is being released. If not specified, the key is being depressed.
        /// </summary>
        KEYUP = 0x0002,
    }

    public enum MOUSEEVENTF : uint
    {
        /// <summary>
        /// The dx and dy parameters contain normalized absolute coordinates. 
        /// If not set, those parameters contain relative data: the change in position since the last reported position. 
        /// This flag can be set, or not set, regardless of what kind of mouse or mouse-like device, if any, is connected to the system. 
        /// For further information about relative mouse motion, see the following Remarks section.
        /// </summary>
        ABSOLUTE = 0x8000,
        /// <summary>
        /// The left button is down.
        /// </summary>
        LEFTDOWN = 0x0002,
        /// <summary>
        /// The left button is up.
        /// </summary>
        LEFTUP = 0x0004,
        /// <summary>
        /// The middle button is down.
        /// </summary>
        MIDDLEDOWN = 0x0020,
        /// <summary>
        /// The middle button is up.
        /// </summary>
        MIDDLEUP = 0x0040,
        /// <summary>
        /// Movement occurred.
        /// </summary>
        MOVE = 0x0001,
        /// <summary>
        /// The right button is down.
        /// </summary>
        RIGHTDOWN = 0x0008,
        /// <summary>
        /// The right button is up.
        /// </summary>
        RIGHTUP = 0x0010,
        /// <summary>
        /// The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
        /// </summary>
        WHEEL = 0x0800,
        /// <summary>
        /// An X button was pressed.
        /// </summary>
        XDOWN = 0x0080,
        /// <summary>
        /// An X button was released.
        /// </summary>
        XUP = 0x0100,
        /// <summary>
        /// The wheel button is tilted.
        /// </summary>
        HWHEEL = 0x01000,
    }

    /// <summary>
    /// Virtual-Key Codes
    /// The symbolic constant names, hexadecimal values, and mouse or keyboard equivalents for the virtual-key codes used by the system. 
    /// </summary>
    public enum VK : byte
    {
        /// <summary>
        /// Left mouse button
        /// </summary>
        LBUTTON = 0x01,
        /// <summary>
        /// Right mouse button
        /// </summary>
        RBUTTON = 0x02,
        /// <summary>
        /// Control-break processing
        /// </summary>
        CANCEL = 0x03,
        /// <summary>
        /// Middle mouse button (three-button mouse)
        /// </summary>
        MBUTTON = 0x04,
        /// <summary>
        /// Windows 2000/XP: X1 mouse button
        ///</summary>
        XBUTTON1 = 0x05,
        /// <summary>
        /// Windows 2000/XP: X2 mouse button
        /// </summary>
        XBUTTON2 = 0x06,
        ///<summary>
        /// BACKSPACE key
        /// </summary>
        BACK = 0x08,
        /// <summary>
        /// TAB key
        /// </summary>
        TAB = 0x09,
        /// <summary>
        /// CLEAR key
        /// </summary>
        CLEAR = 0x0C,
        /// <summary>
        /// ENTER key
        /// </summary>
        RETURN = 0x0D,
        /// <summary>
        /// SHIFT key
        /// </summary>
        SHIFT = 0x10,
        /// <summary>
        /// CTRL key
        /// </summary>
        CONTROL = 0x11,
        /// <summary>
        /// ALT key
        /// </summary>
        MENU = 0x12,
        /// <summary>
        /// PAUSE key
        /// </summary>
        PAUSE = 0x13,
        /// <summary>
        /// CAPS LOCK key
        /// </summary>
        CAPITAL = 0x14,
        /// <summary>
        /// Input Method Editor (IME) Kana mode
        /// </summary>
        KANA = 0x15,
        /// <summary>
        /// IME Hangul mode
        /// </summary>
        HANGUL = 0x15,
        /// <summary>
        /// IME Junja mode
        /// </summary>
        JUNJA = 0x17,
        /// <summary>
        /// IME final mode
        /// </summary>
        FINAL = 0x18,
        /// <summary>
        /// IME Hanja mode
        /// </summary>
        HANJA = 0x19,
        /// <summary>
        /// IME Kanji mode
        /// </summary>
        KANJI = 0x19,
        /// <summary>
        /// ESC key
        /// </summary>
        ESCAPE = 0x1B,
        /// <summary>
        /// IME convert
        /// </summary>
        CONVERT = 0x1C,
        /// <summary>
        /// IME nonconvert
        /// </summary>
        NONCONVERT = 0x1D,
        /// <summary>
        /// IME accept
        /// </summary>
        ACCEPT = 0x1E,
        /// <summary>
        /// IME mode change request
        /// </summary>
        MODECHANGE = 0x1F,
        /// <summary>
        /// SPACEBAR
        /// </summary>
        SPACE = 0x20,
        /// <summary>
        /// PAGE UP key
        /// </summary>
        PRIOR = 0x21,
        /// <summary>
        /// PAGE DOWN key
        /// </summary>
        NEXT = 0x22,
        /// <summary>
        /// END key
        /// </summary>
        END = 0x23,
        /// <summary>
        /// HOME key
        /// </summary>
        HOME = 0x24,
        /// <summary>
        /// LEFT ARROW key
        /// </summary>
        LEFT = 0x25,
        /// <summary>
        /// UP ARROW key
        /// </summary>
        UP = 0x26,
        /// <summary>
        /// RIGHT ARROW key
        /// </summary>
        RIGHT = 0x27,
        /// <summary>
        /// DOWN ARROW key
        /// </summary>
        DOWN = 0x28,
        /// <summary>
        /// SELECT key
        /// </summary>
        SELECT = 0x29,
        /// <summary>
        /// PRINT key
        /// </summary>
        PRINT = 0x2A,
        /// <summary>
        /// EXECUTE key
        /// </summary>
        EXECUTE = 0x2B,
        /// <summary>
        /// PRINT SCREEN key
        /// </summary>
        SNAPSHOT = 0x2C,
        /// <summary>
        /// INS key
        /// </summary>
        INSERT = 0x2D,
        /// <summary>
        /// DEL key
        /// </summary>
        DELETE = 0x2E,
        /// <summary>
        /// HELP key
        /// </summary>
        HELP = 0x2F,
        /// <summary>
        /// 0 key
        /// </summary>
        KEY_0 = 0x30,
        /// <summary>
        /// 1 key
        /// </summary>
        KEY_1 = 0x31,
        /// <summary>
        /// 2 key
        /// </summary>
        KEY_2 = 0x32,
        /// <summary>
        /// 3 key
        /// </summary>
        KEY_3 = 0x33,
        /// <summary>
        /// 4 key
        /// </summary>
        KEY_4 = 0x34,
        /// <summary>
        /// 5 key
        /// </summary>
        KEY_5 = 0x35,
        /// <summary>
        /// 6 key
        /// </summary>
        KEY_6 = 0x36,
        /// <summary>
        /// 7 key
        /// </summary>
        KEY_7 = 0x37,
        /// <summary>
        /// 8 key
        /// </summary>
        KEY_8 = 0x38,
        /// <summary>
        /// 9 key
        /// </summary>
        KEY_9 = 0x39,
        /// <summary>
        /// A key
        /// </summary>
        KEY_A = 0x41,
        /// <summary>
        /// B key
        /// </summary>
        KEY_B = 0x42,
        /// <summary>
        /// C key
        /// </summary>
        KEY_C = 0x43,
        /// <summary>
        /// D key
        /// </summary>
        KEY_D = 0x44,
        /// <summary>
        /// E key
        /// </summary>
        KEY_E = 0x45,
        /// <summary>
        /// F key
        /// </summary>
        KEY_F = 0x46,
        /// <summary>
        /// G key
        /// </summary>
        KEY_G = 0x47,
        /// <summary>
        /// H key
        /// </summary>
        KEY_H = 0x48,
        /// <summary>
        /// I key
        /// </summary>
        KEY_I = 0x49,
        /// <summary>
        /// J key
        /// </summary>
        KEY_J = 0x4A,
        /// <summary>
        /// K key
        /// </summary>
        KEY_K = 0x4B,
        /// <summary>
        /// L key
        /// </summary>
        KEY_L = 0x4C,
        /// <summary>
        /// M key
        /// </summary>
        KEY_M = 0x4D,
        /// <summary>
        /// N key
        /// </summary>
        KEY_N = 0x4E,
        /// <summary>
        /// O key
        /// </summary>
        KEY_O = 0x4F,
        /// <summary>
        /// P key
        /// </summary>
        KEY_P = 0x50,
        /// <summary>
        /// Q key
        /// </summary>
        KEY_Q = 0x51,
        /// <summary>
        /// R key
        /// </summary>
        KEY_R = 0x52,
        /// <summary>
        /// S key
        /// </summary>
        KEY_S = 0x53,
        /// <summary>
        /// T key
        /// </summary>
        KEY_T = 0x54,
        /// <summary>
        /// U key
        /// </summary>
        KEY_U = 0x55,
        /// <summary>
        /// V key
        /// </summary>
        KEY_V = 0x56,
        /// <summary>
        /// W key
        /// </summary>
        KEY_W = 0x57,
        /// <summary>
        /// X key
        /// </summary>
        KEY_X = 0x58,
        /// <summary>
        /// Y key
        /// </summary>
        KEY_Y = 0x59,
        /// <summary>
        /// Z key
        /// </summary>
        KEY_Z = 0x5A,
        /// <summary>
        /// Left Windows key (Microsoft Natural keyboard)
        /// </summary>
        LWIN = 0x5B,
        /// <summary>
        /// Right Windows key (Natural keyboard)
        /// </summary>
        RWIN = 0x5C,
        /// <summary>
        /// Applications key (Natural keyboard)
        /// </summary>
        APPS = 0x5D,
        /// <summary>
        /// Computer Sleep key
        /// </summary>
        SLEEP = 0x5F,
        /// <summary>
        /// Numeric keypad 0 key
        /// </summary>
        NUMPAD0 = 0x60,
        /// <summary>
        /// Numeric keypad 1 key
        /// </summary>
        NUMPAD1 = 0x61,
        /// <summary>
        /// Numeric keypad 2 key
        /// </summary>
        NUMPAD2 = 0x62,
        /// <summary>
        /// Numeric keypad 3 key
        /// </summary>
        NUMPAD3 = 0x63,
        /// <summary>
        /// Numeric keypad 4 key
        /// </summary>
        NUMPAD4 = 0x64,
        /// <summary>
        /// Numeric keypad 5 key
        /// </summary>
        NUMPAD5 = 0x65,
        /// <summary>
        /// Numeric keypad 6 key
        /// </summary>
        NUMPAD6 = 0x66,
        /// <summary>
        /// Numeric keypad 7 key
        /// </summary>
        NUMPAD7 = 0x67,
        /// <summary>
        /// Numeric keypad 8 key
        /// </summary>
        NUMPAD8 = 0x68,
        /// <summary>
        /// Numeric keypad 9 key
        /// </summary>
        NUMPAD9 = 0x69,
        /// <summary>
        /// Multiply key
        /// </summary>
        MULTIPLY = 0x6A,
        /// <summary>
        /// Add key
        /// </summary>
        ADD = 0x6B,
        /// <summary>
        /// Separator key
        /// </summary>
        SEPARATOR = 0x6C,
        /// <summary>
        /// Subtract key
        /// </summary>
        SUBTRACT = 0x6D,
        /// <summary>
        /// Decimal key
        /// </summary>
        DECIMAL = 0x6E,
        /// <summary>
        /// Divide key
        /// </summary>
        DIVIDE = 0x6F,
        /// <summary>
        /// F1 key
        /// </summary>
        F1 = 0x70,
        /// <summary>
        /// F2 key
        /// </summary>
        F2 = 0x71,
        /// <summary>
        /// F3 key
        /// </summary>
        F3 = 0x72,
        /// <summary>
        /// F4 key
        /// </summary>
        F4 = 0x73,
        /// <summary>
        /// F5 key
        /// </summary>
        F5 = 0x74,
        /// <summary>
        /// F6 key
        /// </summary>
        F6 = 0x75,
        /// <summary>
        /// F7 key
        /// </summary>
        F7 = 0x76,
        /// <summary>
        /// F8 key
        /// </summary>
        F8 = 0x77,
        /// <summary>
        /// F9 key
        /// </summary>
        F9 = 0x78,
        /// <summary>
        /// F10 key
        /// </summary>
        F10 = 0x79,
        /// <summary>
        /// F11 key
        /// </summary>
        F11 = 0x7A,
        /// <summary>
        /// F12 key
        /// </summary>
        F12 = 0x7B,
        /// <summary>
        /// F13 key
        /// </summary>
        F13 = 0x7C,
        /// <summary>
        /// F14 key
        /// </summary>
        F14 = 0x7D,
        /// <summary>
        /// F15 key
        /// </summary>
        F15 = 0x7E,
        /// <summary>
        /// F16 key
        /// </summary>
        F16 = 0x7F,
        /// <summary>
        /// F17 key  
        /// </summary>
        F17 = 0x80,
        /// <summary>
        /// F18 key  
        /// </summary>
        F18 = 0x81,
        /// <summary>
        /// F19 key  
        /// </summary>
        F19 = 0x82,
        /// <summary>
        /// F20 key  
        /// </summary>
        F20 = 0x83,
        /// <summary>
        /// F21 key  
        /// </summary>
        F21 = 0x84,
        /// <summary>
        /// F22 key, (PPC only) Key used to lock device.
        /// </summary>
        F22 = 0x85,
        /// <summary>
        /// F23 key  
        /// </summary>
        F23 = 0x86,
        /// <summary>
        /// F24 key  
        /// </summary>
        F24 = 0x87,
        /// <summary>
        /// NUM LOCK key
        /// </summary>
        NUMLOCK = 0x90,
        /// <summary>
        /// SCROLL LOCK key
        /// </summary>
        SCROLL = 0x91,
        /// <summary>
        /// Left SHIFT key
        /// </summary>
        LSHIFT = 0xA0,
        /// <summary>
        /// Right SHIFT key
        /// </summary>
        RSHIFT = 0xA1,
        /// <summary>
        /// Left CONTROL key
        /// </summary>
        LCONTROL = 0xA2,
        /// <summary>
        /// Right CONTROL key
        /// </summary>
        RCONTROL = 0xA3,
        /// <summary>
        /// Left MENU key
        /// </summary>
        LMENU = 0xA4,
        /// <summary>
        /// Right MENU key
        /// </summary>
        RMENU = 0xA5,
        /// <summary>
        /// Windows 2000/XP: Browser Back key
        /// </summary>
        BROWSER_BACK = 0xA6,
        /// <summary>
        /// Windows 2000/XP: Browser Forward key
        /// </summary>
        BROWSER_FORWARD = 0xA7,
        /// <summary>
        /// Windows 2000/XP: Browser Refresh key
        /// </summary>
        BROWSER_REFRESH = 0xA8,
        /// <summary>
        /// Windows 2000/XP: Browser Stop key
        /// </summary>
        BROWSER_STOP = 0xA9,
        /// <summary>
        /// Windows 2000/XP: Browser Search key
        /// </summary>
        BROWSER_SEARCH = 0xAA,
        /// <summary>
        /// Windows 2000/XP: Browser Favorites key
        /// </summary>
        BROWSER_FAVORITES = 0xAB,
        /// <summary>
        /// Windows 2000/XP: Browser Start and Home key
        /// </summary>
        BROWSER_HOME = 0xAC,
        /// <summary>
        /// Windows 2000/XP: Volume Mute key
        /// </summary>
        VOLUME_MUTE = 0xAD,
        /// <summary>
        /// Windows 2000/XP: Volume Down key
        /// </summary>
        VOLUME_DOWN = 0xAE,
        /// <summary>
        /// Windows 2000/XP: Volume Up key
        /// </summary>
        VOLUME_UP = 0xAF,
        /// <summary>
        /// Windows 2000/XP: Next Track key
        /// </summary>
        MEDIA_NEXT_TRACK = 0xB0,
        /// <summary>
        /// Windows 2000/XP: Previous Track key
        /// </summary>
        MEDIA_PREV_TRACK = 0xB1,
        /// <summary>
        /// Windows 2000/XP: Stop Media key
        /// </summary>
        MEDIA_STOP = 0xB2,
        /// <summary>
        /// Windows 2000/XP: Play/Pause Media key
        /// </summary>
        MEDIA_PLAY_PAUSE = 0xB3,
        /// <summary>
        /// Windows 2000/XP: Start Mail key
        /// </summary>
        LAUNCH_MAIL = 0xB4,
        /// <summary>
        /// Windows 2000/XP: Select Media key
        /// </summary>
        LAUNCH_MEDIA_SELECT = 0xB5,
        /// <summary>
        /// Windows 2000/XP: Start Application 1 key
        /// </summary>
        LAUNCH_APP1 = 0xB6,
        /// <summary>
        /// Windows 2000/XP: Start Application 2 key
        /// </summary>
        LAUNCH_APP2 = 0xB7,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_1 = 0xBA,
        /// <summary>
        /// Windows 2000/XP: For any country/region, the '+' key
        /// </summary>
        OEM_PLUS = 0xBB,
        /// <summary>
        /// Windows 2000/XP: For any country/region, the ',' key
        /// </summary>
        OEM_COMMA = 0xBC,
        /// <summary>
        /// Windows 2000/XP: For any country/region, the '-' key
        /// </summary>
        OEM_MINUS = 0xBD,
        /// <summary>
        /// Windows 2000/XP: For any country/region, the '.' key
        /// </summary>
        OEM_PERIOD = 0xBE,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_2 = 0xBF,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_3 = 0xC0,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_4 = 0xDB,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_5 = 0xDC,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_6 = 0xDD,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_7 = 0xDE,
        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        OEM_8 = 0xDF,
        /// <summary>
        /// Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard
        /// </summary>
        OEM_102 = 0xE2,
        /// <summary>
        /// Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
        /// </summary>
        PROCESSKEY = 0xE5,
        /// <summary>
        /// Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. 
        /// The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. 
        /// For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
        /// </summary>
        PACKET = 0xE7,
        /// <summary>
        /// Attn key
        /// </summary>
        ATTN = 0xF6,
        /// <summary>
        /// CrSel key
        /// </summary>
        CRSEL = 0xF7,
        /// <summary>
        /// ExSel key
        /// </summary>
        EXSEL = 0xF8,
        /// <summary>
        /// Erase EOF key
        /// </summary>
        EREOF = 0xF9,
        /// <summary>
        /// Play key
        /// </summary>
        PLAY = 0xFA,
        /// <summary>
        /// Zoom key
        /// </summary>
        ZOOM = 0xFB,
        /// <summary>
        /// Reserved
        /// </summary>
        NONAME = 0xFC,
        /// <summary>
        /// PA1 key
        /// </summary>
        PA1 = 0xFD,
        /// <summary>
        /// Clear key
        /// </summary>
        OEM_CLEAR = 0xFE
    }


    // Win32 Metthods

    // kernel32.dll
    [DllImport("kernel32.dll")]
    public static extern uint GetTickCount();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool QueryPerformanceFrequency(out long frequency);

    [DllImport("kernel32.dll")]
    public static extern void Sleep(uint dwMilliseconds);

    // user32.dll
    [DllImport("user32.dll")]
    static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, POINT pt, uint uFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, POINT pt, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowExA(IntPtr parentHandle, IntPtr hWndChildAfter, string className, string windowTitle);

    [DllImport("user32.dll")]
    public static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetTopWindow(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindow(IntPtr hWnd, GW uCmd);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern uint GetWindowModuleFileName(
        IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

    [DllImport("user32.dll", EntryPoint ="keybd_event")]
    static extern void KeyboardEvent(byte bVk, byte bScan, uint dwFlags,
   UIntPtr dwExtraInfo);

    [DllImport("user32.dll", EntryPoint = "mouse_event")]
    public static extern void MouseEvent(
        uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, StringBuilder lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, ref nint lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, nint lParam);

    [DllImport("user32.dll")]
    public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowEnabled(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsZoomed(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetActiveWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetFocus(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll")]
    public static extern bool SetRect(out RECT lprc,
        int xLeft, int yTop, int xRight, int yBottom);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SWP uFlags);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool SetWindowText(IntPtr hwnd, String lpString);

    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(System.Drawing.Point p);
}
