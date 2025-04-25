using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace work.ctrl3d.OS
{
    /// <summary>
    /// Provides OS-level command execution and utility methods.
    /// This class allows running shell commands both synchronously and asynchronously,
    /// as well as exposing some system control functionalities.
    /// </summary>
    public class OSCmd
    {
        /// <summary>
        /// Reference to the shell interface used for executing commands and system actions.
        /// </summary>
        private readonly IShell _shell;

        /// <summary>
        /// Initializes a new instance of the <see cref="OSCmd"/> class.
        /// Uses the specified <see cref="IShell"/> implementation or
        /// provides a default via <see cref="ShellFactory"/>.
        /// </summary>
        /// <param name="shell">An optional custom shell implementation for command execution.</param>
        public OSCmd(IShell shell = null)
        {
            _shell = shell ?? ShellFactory.Create();
        }

        /// <summary>
        /// Runs the specified command synchronously with optional arguments.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="arguments">Optional command arguments.</param>
        /// <returns>The standard output from the command execution.</returns>
        public string Run(string command, string arguments = "")
        {
            return _shell.Run(command, arguments);
        }

        /// <summary>
        /// Runs the specified command asynchronously with optional arguments.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="arguments">Optional command arguments.</param>
        /// <returns>A task containing the standard output from the command execution.</returns>
        public async Task<string> RunAsync(string command, string arguments = "")
        {
            return await _shell.RunAsync(command, arguments);
        }
        
        /// <summary>
        /// Checks if the current operating system is Windows.
        /// </summary>
        /// <returns><c>true</c> if Windows; otherwise, <c>false</c>.</returns>
        public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// Checks if the current operating system is Linux.
        /// </summary>
        /// <returns><c>true</c> if Linux; otherwise, <c>false</c>.</returns>
        public static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        /// Checks if the current operating system is macOS.
        /// </summary>
        /// <returns><c>true</c> if macOS; otherwise, <c>false</c>.</returns>
        public static bool IsOSX() => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        /// <summary>
        /// Gets the architecture of the current operating system.
        /// </summary>
        /// <returns>A string representing the OS architecture (e.g., "X64").</returns>
        public static string GetOSArchitecture() => RuntimeInformation.OSArchitecture.ToString();

        /// <summary>
        /// Gets the description of the current operating system.
        /// </summary>
        /// <returns>A string describing the OS version and details.</returns>
        public static string GetOSDescription() => RuntimeInformation.OSDescription;
        
        /// <summary>
        /// Initiates a system reboot.
        /// </summary>
        public void Reboot() => _shell.System.Reboot();

        /// <summary>
        /// Initiates a system shutdown.
        /// </summary>
        public void Shutdown() => _shell.System.Shutdown();

        /// <summary>
        /// Puts the system into sleep mode.
        /// </summary>
        public void Sleep() => _shell.System.Sleep();

        // The following methods are commented out. Uncomment to enable these features if supported by your shell/system:
        // /// <summary>
        // /// Puts the system into hibernation mode.
        // /// </summary>
        // public void Hibernate() => _shell.System.Hibernate();

        // /// <summary>
        // /// Locks the system.
        // /// </summary>
        // public void Lock() => _shell.System.Lock();
    }
}