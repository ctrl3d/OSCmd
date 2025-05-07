#if UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace work.ctrl3d.OS
{
    public interface IShell
    {
        string Run(string command, string arguments = "");

#if UNITASK_SUPPORT
        UniTask<string> RunAsync(string command, string arguments = "");
#else
        Task<string> RunAsync(string command, string arguments = "");
#endif
        ISystem System { get; }
    }
}