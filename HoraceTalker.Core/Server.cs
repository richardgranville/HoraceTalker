namespace HoraceTalker.Core
{
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

        public static ArrayList Connections { get; set; }

        public Server(CommandService commandService, int portNumber)
        {
            this.commandService = commandService;
            this.portNumber = portNumber;
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
                var connection = new Connection(conn, this.commandService);
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