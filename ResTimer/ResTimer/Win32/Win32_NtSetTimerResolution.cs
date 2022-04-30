using System.Runtime.InteropServices;

namespace ResTimer
{
    internal sealed class Win32_NtSetTimerResolution
	{
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtSetTimerResolution(uint DesiredResolution, bool SetResolution, out uint CurrentResolution);

		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint ActualResolution);

		public void SetMaxTimerResolution(uint WantedResolution)
		{
			uint actual = 0U;
			Win32_NtSetTimerResolution.NtSetTimerResolution(WantedResolution, true, out actual);
		}

		public void ReleaseMaxTimerResolution(uint WantedResolution)
		{
			uint actual = 0U;
			Win32_NtSetTimerResolution.NtSetTimerResolution(WantedResolution, false, out actual);
		}

		private uint DefaultResolution;

		private uint MininumResolution;

		private uint MaximumResolution;
	}
}
