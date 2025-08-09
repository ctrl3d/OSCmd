using System;
using System.Diagnostics;
using System.Text;
#if USE_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace work.ctrl3d
{
    public class WindowsShell : IShell, ISystem
    {
        private const string FileName = "cmd.exe";
        private readonly Encoding _encoding;

        public WindowsShell(Encoding encoding = null) => _encoding = encoding ?? Encoding.Default;

        public string Run(string command, string arguments = "")
        {
            var args = $"/c {command} {arguments} 2>&1";

            var startInfo = new ProcessStartInfo
            {
                FileName = FileName,
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                StandardOutputEncoding = _encoding,
                StandardErrorEncoding = _encoding
            };

            using var process = new Process();
            process.StartInfo = startInfo;

            try
            {
                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }
        }

#if USE_UNITASK
        public async UniTask<string> RunAsync(string command, string arguments = "")
        {
            var args = $"/c {command} {arguments} 2>&1";

            var startInfo = new ProcessStartInfo
            {
                FileName = FileName,
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                StandardOutputEncoding = _encoding,
                StandardErrorEncoding = _encoding
            };

            using var process = new Process();
            process.StartInfo = startInfo;

            try
            {
                process.Start();

                var outputTask = process.StandardOutput.ReadToEndAsync();
                await UniTask.RunOnThreadPool(() => process.WaitForExit());
                return await outputTask;
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }
        }
#else
        public async Task<string> RunAsync(string command, string arguments = "")
        {
            var args = $"/c {command} {arguments} 2>&1";

            var startInfo = new ProcessStartInfo
            {
                FileName = FileName,
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                StandardOutputEncoding = _encoding,
                StandardErrorEncoding = _encoding
            };

            using var process = new Process();
            process.StartInfo = startInfo;

            try
            {
                process.Start();

                var outputTask = process.StandardOutput.ReadToEndAsync();
                await Task.Run(() => process.WaitForExit());
                return await outputTask;
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }
        }
#endif


        public ISystem System => this;
        public void Reboot() => Run("shutdown", "/r /t 0");
        public void Shutdown() => Run("shutdown", "/s /t 0");

        public void Sleep() => Run("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0");
        //public void Hibernate() => Run("rundll32.exe", "powrprof.dll,SetSuspendState Hibernate");
        //public void Lock() => Run("rundll32.exe", "user32.dll,LockWorkStation");
    }
}