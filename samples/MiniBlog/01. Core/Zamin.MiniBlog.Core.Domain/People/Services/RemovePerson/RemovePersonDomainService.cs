using System.Threading.Tasks;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.MiniBlog.Core.Domain.Posts.Repositories;

namespace Zamin.MiniBlog.Core.Domain.People.Services.RemovePerson
{
    public class RemovePersonDomainService : IRemovePersonDomainService
    {
        public async Task Execute(
            IPersonCommandRepository personCommandRepository,
            IPostCommandRepository postCommandRepository,
            BusinessId PersonBusinessId)
        {
            var person = await personCommandRepository.GetAsync(PersonBusinessId);

            if(person is null)
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorNotExists,
                    ZaminMiniBlogResources.Person);

            var posts = await postCommandRepository.GetByPersonBusinessIdAsync(PersonBusinessId);

            foreach (var post in posts)
            {
                post.Removed();

                postCommandRepository.Delete(post);
            }

            person.Remove();

            personCommandRepository.Delete(person);
        }
    }
}
