namespace HoraceTalker.Core.Services
{
    public class CommunicationService : ICommunicationService
    {
        public void Broadcast(string message)
        {
            foreach (Connection connection in Server.Connections)
            {
                connection.Writer.WriteLine(message);
            }
        }

        public void UserMessage(string message, Connection userConnection)
        {
            userConnection.Writer.WriteLine(message);
        }

        public void Message(string userMessage, string publicMessage, Connection userConnection)
        {
            foreach (Connection connection in Server.Connections)
            {
                if (userConnection == connection)
                {
                    connection.Writer.WriteLine(userMessage);
                }
                else
                {
                    connection.Writer.WriteLine(publicMessage);
                }
            }
        }
    }
}
