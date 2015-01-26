using HoraceTalker.Domain.Abstract;
using HoraceTalker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Domain.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private HoraceTalkerContext context = new HoraceTalkerContext();

        public EfUserRepository()
        {
        }

        public void SaveUser(Models.User user)
        {
            if (user.Id == 0)
            {
                context.Users.Add(user);
                context.Entry(user).State = EntityState.Added;
            }
            else
            {
                context.Entry(user).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public Models.User GetUser(string userName)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName);
        }
    }
}
