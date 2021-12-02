using System.Threading.Tasks;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.MiniBlog.Core.Domain.Posts.Repositories;
using Zamin.Utilities.Services.DependentyInjection;

namespace Zamin.MiniBlog.Core.Domain.People.Services.RemovePerson
{
    public interface IRemovePersonDomainService : ITransientLifetime
    {
        public Task Execute(
            IPersonCommandRepository personCommandRepository,
            IPostCommandRepository postCommandRepository,
            BusinessId PersonId);
    }
}
