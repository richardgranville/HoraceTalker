namespace HoraceTalker.Core.Helpers
{
    public static class DisplayHelper
    {
        public static void WriteLine(Connection connection, string line) 
        {
            connection.Writer.WriteLine(line);
        }
    }
}
