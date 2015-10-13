namespace HoraceTalker.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using HoraceTalker.Core;
    using HoraceTalker.Core.Commands;

    public class CommandService
    {
        private IEnumerable<ICommand> commands;

        private ICommunicationService commsService;

        public CommandService(IEnumerable<ICommand> commands, ICommunicationService commsService)
        {
            this.commands = commands;
            this.commsService = commsService;
        }

        public void ProcessLine(Connection callingConnection, string line)
        {
            var arguments = string.Empty;
            var commandName = GetCommandName(line, out arguments);

            if (string.IsNullOrEmpty(commandName)) 
            {
                commsService.UserMessage(string.Format("A match for '{0}' could not be found.", commandName), callingConnection);
            }

            var command = GetCommand(commandName);

            if (command == null)
            {
                commsService.UserMessage(string.Format("A match for '{0}' could not be found.", commandName), callingConnection);
                return;
            }

            command.Process(callingConnection, arguments);
        }

        private ICommand GetCommand(string commandName)
        {
            return commands.FirstOrDefault(c => c.Name == commandName);
        }

        private static string GetCommandName(string line, out string arguments)
        {
            arguments = string.Empty;

            if (!line.Contains(' '))
            {
                return line;
            }

            var splitLine = line.Split(' ').ToList();
            var commandName = splitLine[0];
            splitLine.RemoveAt(0);

            arguments = string.Join(" ", splitLine);
            return commandName;
        }
    }
}