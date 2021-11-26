﻿namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.TestExternal;

public class TestCommandHandler : CommandHandler<TestCommand>
{
    public TestCommandHandler(ZaminServices zaminServices) : base(zaminServices)
    {
    }

    public override Task<CommandResult> Handle(TestCommand request)
    {
        int a = 123;
        return OkAsync();
    }
}
