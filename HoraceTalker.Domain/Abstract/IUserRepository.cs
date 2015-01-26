using HoraceTalker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Domain.Abstract
{
    public interface IUserRepository
    {
        void SaveUser(User user);

        User GetUser(string userName);
    }
}
