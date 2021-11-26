namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.PTestCommands;

public class PTestCommandHandler : CommandHandler<PTestCommand>
{
    public PTestCommandHandler(ZaminServices zaminServices) : base(zaminServices)
    {
    }

    public override Task<CommandResult> Handle(PTestCommand request)
    {
        return OkAsync();
    }
}