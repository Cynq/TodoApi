using System.Threading.Tasks;

namespace Todo.Common.Interfaces.Identity
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
}
