namespace HoraceTalker.Core.Services
{
    public interface ICommunicationService
    {
        void Broadcast(string message);

        void UserMessage(string message, Connection userConnection);

        void Message(string userMessage, string publicMessage, Connection userConnection);
    }
}
