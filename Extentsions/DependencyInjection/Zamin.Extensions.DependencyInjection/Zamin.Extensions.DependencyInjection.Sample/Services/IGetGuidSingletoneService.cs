using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Zamin.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidSingletoneService : ISingletoneLifetime
{
    Guid Execute();
}
