using System;
using System.Runtime.InteropServices;

namespace work.ctrl3d.OS
{
    public static class ShellFactory
    {
        public static IShell Create()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return new WindowsShell();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return new LinuxShell();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return new OSXShell();
            throw new PlatformNotSupportedException();
        }
    }
}