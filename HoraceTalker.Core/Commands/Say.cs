using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Core.Commands
{
    using Helpers;
    using HoraceTalker.Core.Services;

    public class Say : CommandBase, ICommand
    {
        public Say(ICommunicationService commsService)
            : base(commsService)
        {
        }

        public string Name
        {
            get
            {
                return "say";
            }
        }

        public void Process(Connection callingConnection, string argumentsString)
        {
            this.commsService.Broadcast(string.Format("{0} says '{1}'", "horace", argumentsString));
        }
    }
}
