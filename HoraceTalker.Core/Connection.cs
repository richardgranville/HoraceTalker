namespace HoraceTalker.Core
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    using Services;
    using Domain.Models;

    public class Connection
    {
        private static object bigLock = new object();
        private Socket socket;
        private StreamReader reader;
        private CommandService commandService;

        private readonly IUserService userService;

        private User userToValidate;

        public StreamWriter Writer { get; private set; }

        public Guid ConnectionId { get; private set; }

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

            this.ConnectionId = new Guid();
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
                if (LoggedIn)
                {
                    commandService.ProcessLine(callingConnection, line);
                }
                else
                {
                    if (userToValidate == null)
                    {
                        if (!userService.CheckUserName(line))
                        {
                            Writer.WriteLine("Invalid user name. Please try again.");
                            Writer.WriteLine("username: ");
                            return;
                        }

                        var user = userService.GetUser(line);

                        if (user == null)
                        {
                            LoggedInUser = new User {UserName = line};
                            Welcome();
                            return;
                        }

                        userToValidate = user;
                        Writer.Write("password: ");
                    }
                    else if (userToValidate != null)
                    {
                        var validUser = userService.ValidateUser(userToValidate, line);

                        if (!validUser)
                        {
                            Writer.WriteLine("Invalid password.");
                            Writer.Write("password:");
                            return;
                        }

                        LoggedInUser = userToValidate;
                        Welcome();
                    }
                    else
                    {
                        Disconnect();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private void Welcome()
        {
            Writer.WriteLine("Hello!");
        }

        private void Connect()
        {
            Writer.WriteLine("Welcome!");
            Server.Connections.Add(this);
        }
    }
}
