namespace HoraceTalker.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    using HoraceTalker.Core.Services;

    public class Server
    {
        private const int BacklogSize = 20;

        private CommandService commandService;

        private readonly int portNumber;

        private readonly IUserService userService;

        public static ArrayList Connections { get; set; }

        public Server(CommandService commandService, int portNumber, IUserService userService)
        {
            if (commandService == null)
            {
                throw new ArgumentNullException("commandService");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.commandService = commandService;
            this.portNumber = portNumber;
            this.userService = userService;
        }

        public void Start()
        {
            Connections = new ArrayList();
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Any, this.portNumber));
            server.Listen(BacklogSize);

            while (true)
            {
                var conn = server.Accept();
                var connection = new Connection(conn, this.commandService, userService);
            }
        }

        public static IEnumerable<Connection> CurrentConnections
        {
            get
            {
                return from Connection connection in Connections
                       where connection.LoggedIn
                       select connection;
            }
        }
    }
}