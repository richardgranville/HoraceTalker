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
            this.Bind<Server>().ToSelf().WithConstructorArgument("portNumber", 4000);
            this.Bind<ICommunicationService>().To<CommunicationService>();
            this.Bind<IUserRepository>().To<EfUserRepository>().InThreadScope();
            ////this.Bind<IUserService>().To<UserService>().InSingletonScope();
            this.Bind<CommandService>().ToSelf();

            this.Bind<ICommand>().To<Say>();
        }
    }
}
