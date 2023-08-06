using MiniBlog.Core.Domain.People.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace MiniBlog.Core.Contracts.People;

public interface IPersonCommandRepository : ICommandRepository<Person, int>
{
}
