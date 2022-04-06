using Zamin.Extentions.DependentyInjection.Abstractions;

namespace Zamin.Extensions.DependentyInjection.Sample.Services;

public interface IGetGuidScopeService : IScopeLifetime
{
    Guid Execute();
}
