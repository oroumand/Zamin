using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class DescriptionConversion: ValueConverter<Description, string>
    {
        public DescriptionConversion():base(c=>c.Value,c=>Description.FromString(c))
        {

        }
    }
}
