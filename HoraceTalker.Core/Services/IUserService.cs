namespace HoraceTalker.Core.Services
{
    using Domain.Models;

    public interface IUserService
    {
        User GetUser(string userName);

        bool ValidateUser(User user, string password);

        void RegisterUser(string userName, string password);

        bool CheckUserName(string userName);

        bool CheckPassword(string password);
    }
}
