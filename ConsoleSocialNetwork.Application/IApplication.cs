using System.Text;

namespace ConsoleSocialNetwork.Application
{
    public interface IApplication
    {
        string ExecuteCommand(string commandString);
    }
}
