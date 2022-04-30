using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ResTimer
{
    public partial class Form1 : Form
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)] internal static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);[DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)] internal static extern bool SetProcessWorkingSetSize64Bit(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);

        public Form1()
        {
            InitializeComponent();
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("ResTimer is already running. Only one instance of this application is allowed");
                base.Close();
                return;
            }
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Opacity = 0.0;
            this.notifyIcon1.ShowBalloonTip(5, "ResTimer", "NYZX Tools", ToolTipIcon.Info);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Win32_NtSetTimerResolution().SetMaxTimerResolution((uint)(0.50 * 10000f));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        public static void Gc()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static void FlushMem()
        {
            Gc();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {

                SetProcessWorkingSetSize32Bit(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FlushMem();
        }
    }
}
