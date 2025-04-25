using System.Threading.Tasks;

namespace work.ctrl3d.OS
{
    public interface IShell
    {
        string Run(string command, string arguments = "");
        Task<string> RunAsync(string command, string arguments = "");
        ISystem System { get; }

    }
}