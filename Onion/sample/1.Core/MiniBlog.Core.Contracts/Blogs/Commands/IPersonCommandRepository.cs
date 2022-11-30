using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands;

public interface IPersonCommandRepository : ICommandRepository<Person>
{
}
