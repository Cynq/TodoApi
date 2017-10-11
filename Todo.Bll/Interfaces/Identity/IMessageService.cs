using System.Threading.Tasks;

namespace Todo.Bll.Interfaces.Identity
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
}
