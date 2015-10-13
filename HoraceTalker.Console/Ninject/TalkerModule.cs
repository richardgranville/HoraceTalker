namespace HoraceTalker.Console.Ninject
{
    using global::Ninject.Modules;

    using HoraceTalker.Core;
    using HoraceTalker.Core.Commands;
    using HoraceTalker.Core.Services;
    using HoraceTalker.Domain.Abstract;
    using HoraceTalker.Domain.Concrete;

    public class TalkerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Server>().ToSelf().WithConstructorArgument("portNumber", 4000);
            Bind<ICommunicationService>().To<CommunicationService>();
            Bind<IUserRepository>().To<EfUserRepository>().InThreadScope();
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<CommandService>().ToSelf();

            Bind<ICommand>().To<Say>();
        }
    }
}
