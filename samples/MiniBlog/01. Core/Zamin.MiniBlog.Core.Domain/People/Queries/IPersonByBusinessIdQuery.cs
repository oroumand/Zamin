using System;

namespace Zamin.MiniBlog.Core.Domain.People.Queries
{
    public interface IPersonByBusinessIdQuery
    {
        public Guid BusinessId { get; set; }
    }
}
