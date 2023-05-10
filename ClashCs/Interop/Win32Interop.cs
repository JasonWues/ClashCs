using System.Runtime.InteropServices;

namespace ClashCs.Interop
{
    public partial class Win32Interop
    {
        [LibraryImport("shell32.dll",EntryPoint = "ShellExecuteW")]
        public static partial IntPtr ShellExecute(IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPTStr)] string lpOperation,
            [MarshalAs(UnmanagedType.LPTStr)] string lpFile,
            [MarshalAs(UnmanagedType.LPTStr)] string lpParameters,
            [MarshalAs(UnmanagedType.LPTStr)] string lpDirectory,
            int nShowCmd);
    }
}
