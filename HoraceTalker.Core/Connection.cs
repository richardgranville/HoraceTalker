namespace HoraceTalker.Core
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    using HoraceTalker.Core.Services;
    using HoraceTalker.Domain.Models;

    public class Connection
    {
        private static object bigLock = new object();
        private Socket socket;
        private StreamReader reader;
        private CommandService commandService;

        private readonly IUserService userService;

        private string userName;

        public StreamWriter Writer { get; private set; }

        public bool LoggedIn
        {
            get
            {
                return this.LoggedInUser != null;
            }
        }

        public User LoggedInUser { get; private set; }

        public Connection(Socket socket, CommandService commandService, IUserService userService)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }
            if (commandService == null)
            {
                throw new ArgumentNullException("commandService");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.commandService = commandService;
            this.userService = userService;
            this.socket = socket;
            reader = new StreamReader(new NetworkStream(socket, false));
            Writer = new StreamWriter(new NetworkStream(socket, true));
            new Thread(ClientLoop).Start();
        }

        public void Disconnect()
        {
            Server.Connections.Remove(this);
        }

        private void ClientLoop()
        {
            try
            {
                lock (bigLock)
                {
                    this.Connect();
                }
                while (true)
                {
                    lock (bigLock)
                    {
                        foreach (Connection connection in Server.Connections)
                        {
                            connection.Writer.Flush();
                        }
                    }

                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    lock (bigLock)
                    {
                        this.ProcessLine(line, this);
                    }
                }
            }
            finally
            {
                lock (bigLock)
                {
                    socket.Close();
                    this.Disconnect();
                }
            }
        }

        private void ProcessLine(string line, Connection callingConnection)
        {
            try
            {
                commandService.ProcessLine(callingConnection, line);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private void Connect()
        {
            Writer.WriteLine("Welcome!");
            Server.Connections.Add(this);
        }
    }
}
