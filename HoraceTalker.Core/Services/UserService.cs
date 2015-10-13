namespace HoraceTalker.Core.Services
{
    using HoraceTalker.Domain.Abstract;
    using HoraceTalker.Domain.Models;

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
            return userRepository.GetUser(userName);
        }

        public void RegisterUser(string userName, string password)
        {
            var newUser = new User {
                UserName = userName,
                Password = password
            };

            userRepository.SaveUser(newUser);
        }
    }
}
