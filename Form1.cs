using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Process[] process = Process.GetProcessesByName("LINE");
            var ma = process[0].MainWindowHandle;
            unsafe
            {
                int tt = 9;
                int* ttpoint = &tt;
                *ttpoint = 10;
            }
            Point p = new Point();
            var point = user32.GetCursorPos(out p);
            int x = (int)(65536.0 / 1920 * 200) + 1;
            int y = (int)(65536.0 / 1080 * 200) + 1;

            user32.mouse_event(user32.MOUSEEVENTF_ABSOLUTE | user32.MOUSEEVENTF_MOVE, x, y, 0, IntPtr.Zero);
            ////user32.SetCursorPos(10, 10);
            var collection = new List<DesktopModel>();

            user32.EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                var obj = GCHandle.FromIntPtr((IntPtr)0x0C).Target;
                int nLength = user32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                RECT rECT = new RECT();
                var windows = user32.GetWindowRect(hWnd, out rECT);
                string strTitle = strbTitle.ToString();

                if (user32.IsWindowVisible(hWnd) && string.IsNullOrEmpty(strTitle) == false)
                {
                    collection.Add(new DesktopModel
                    {
                        desktopName = strTitle,
                        rECT = rECT
                    });
                }
                return true;
            };

            if (user32.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero))
            {
                foreach (var item in collection)
                {
                    Console.WriteLine(item);
                }
            }

        }
    }
}
