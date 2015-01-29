namespace HoraceTalker.Core.Services
{
    using System.Text.RegularExpressions;

    using Domain.Abstract;
    using Domain.Models;

    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private ICommunicationService commsService;

        public UserService(IUserRepository userRepository, ICommunicationService commsService)
        {
            this.userRepository = userRepository;
            this.commsService = commsService;
        }

        public User GetUser(string userName)
        {
            return this.userRepository.GetUser(userName);
        }

        public bool ValidateUser(User user, string password)
        {
            return user.Password == password;
        }

        public void RegisterUser(string userName, string password)
        {
            var newUser = new User {
                UserName = userName,
                Password = password
            };

            this.userRepository.SaveUser(newUser);
        }

        public bool CheckUserName(string userName)
        {
            var alphaRegEx = new Regex("^[a-zA-Z]*$");
            if (!alphaRegEx.IsMatch(userName))
            {
                return false;
            }

            return true;
        }

        public bool CheckPassword(string password)
        {
            return true;
        }
    }
}
