namespace HoraceTalker.Core.Services
{
    using HoraceTalker.Domain.Models;

    public interface IUserService
    {
        User GetUser(string userName);

        void RegisterUser(string userName, string password);
    }
}
