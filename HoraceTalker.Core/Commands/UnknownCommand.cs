using HoraceTalker.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Core.Commands
{
    public class UnknownCommand : CommandBase, ICommand
    {
        public UnknownCommand(ICommunicationService commsService)
            : base(commsService)
        {
        }

        public string Name
        {
            get { return string.Empty; }
        }

        public void Process(Connection callingConnection, string argumentsString)
        {
            this.commsService.UserMessage("Unkown command", callingConnection);
        }
    }
}
