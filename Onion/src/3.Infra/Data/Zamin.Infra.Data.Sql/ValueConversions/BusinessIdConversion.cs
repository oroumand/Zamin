using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Infra.Data.Sql.ValueConversions
{
    public class BusinessIdConversion : ValueConverter<BusinessId, Guid>
    {
        public BusinessIdConversion() : base(c => c.Value, c => BusinessId.FromGuid(c))
        {

        }
    }
}
