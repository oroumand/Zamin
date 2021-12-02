using Microsoft.EntityFrameworkCore;
using System.Linq;
using Zamin.Core.Domain.Data;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.MiniBlog.Core.Domain.People.Queries;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.People
{
    public class PersonQueryRepository : BaseQueryRepository<MiniblogQueryDbContext>, IPersonQueryRepository
    {
        public PersonQueryRepository(MiniblogQueryDbContext dbContext) : base(dbContext)
        {
        }

        public PersonQr Select(IPersonByBusinessIdQuery query)
            => PersonDb.FirstOrDefault(c => c.BusinessId == query.BusinessId);

        public PagedData<PersonQr> Select(IPeoplePagedQuery query)
        {
            throw new System.NotImplementedException();
        }

        private IQueryable<PersonQr> PersonDb
            => _dbContext.People.AsNoTracking()
            .Select(c => new PersonQr()
            {
                Id = c.Id,
                BusinessId = c.BusinessId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                BirthDate = c.BirthDate,
            });
    }
}
