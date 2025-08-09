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
    public class OSXShell : IShell, ISystem
    {
        private const string FileName = "/bin/bash";
        private readonly Encoding _encoding;

        public OSXShell(Encoding encoding = null) => _encoding = encoding ?? Encoding.Default;

        public string Run(string command, string arguments = "")
        {
            var args = $"-c \"{command} {arguments} 2>&1\"";

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
                return ex.Message;
            }
        }

#if USE_UNITASK
        public async UniTask<string> RunAsync(string command, string arguments = "")
        {
            var args = $"-c \"{command} {arguments} 2>&1\"";

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
            var args = $"-c \"{command} {arguments} 2>&1\"";

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

        public void Reboot() => Run("shutdown", "-r now");

        public void Shutdown() => Run("shutdown", "-h now");

        public void Sleep() => Run("pmset", "sleepnow");
    }
}