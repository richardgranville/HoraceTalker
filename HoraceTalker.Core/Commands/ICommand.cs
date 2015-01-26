using HoraceTalker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Core.Commands
{
    public interface ICommand
    {
        string Name { get; }

        void Process(Connection callingConnection, string argumentsString);
    }
}
