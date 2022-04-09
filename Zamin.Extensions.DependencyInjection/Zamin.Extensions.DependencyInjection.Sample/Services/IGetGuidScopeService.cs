using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Zamin.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidScopeService : IScopeLifetime
{
    Guid Execute();
}
