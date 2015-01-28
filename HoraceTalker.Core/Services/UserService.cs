namespace HoraceTalker.Core.Services
{
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public bool CheckPassword(string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
