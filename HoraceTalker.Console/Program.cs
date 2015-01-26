namespace HoraceTalker.Console
{
    using HoraceTalker.Console.Ninject;
    using HoraceTalker.Core;

    using global::Ninject;

    public class Program
    {
        private static IKernel kernel;

        public static void Main(string[] args)
        {
            kernel = new StandardKernel(new TalkerModule());
            var server = kernel.Get<Server>();
            server.Start();
        }
    }
}
