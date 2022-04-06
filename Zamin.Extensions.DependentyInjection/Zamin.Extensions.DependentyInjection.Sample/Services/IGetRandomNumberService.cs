using Zamin.Extentions.DependentyInjection.Abstractions;

namespace Zamin.Extensions.DependentyInjection.Sample.Services;

public interface IGetGuidTransientService : ITransientLifetime
{
    Guid Execute();
}