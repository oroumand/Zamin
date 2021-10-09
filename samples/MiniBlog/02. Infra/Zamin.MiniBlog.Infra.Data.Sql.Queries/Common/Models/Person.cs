using Zamin.Infra.Data.Sql.Queries;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common.Models
{
    public class Person : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
    }
}