#if USE_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace work.ctrl3d
{
    public interface IShell
    {
        string Run(string command, string arguments = "");

#if USE_UNITASK
        UniTask<string> RunAsync(string command, string arguments = "");
#else
        Task<string> RunAsync(string command, string arguments = "");
#endif
        ISystem System { get; }
    }
}