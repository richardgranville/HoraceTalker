using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Core.Commands
{
    using HoraceTalker.Core.Services;

    public class CommandBase
    {
        protected ICommunicationService commsService;

        public CommandBase(ICommunicationService commsService)
        {
            this.commsService = commsService;
        }
    }
}
