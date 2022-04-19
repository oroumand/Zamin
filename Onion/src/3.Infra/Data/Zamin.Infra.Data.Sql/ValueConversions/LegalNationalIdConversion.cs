using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Zamin.Infra.Data.Sql.ValueConversions
{
    public class LegalNationalIdConversion : ValueConverter<LegalNationalId, string>
    {
        public LegalNationalIdConversion() : base(c => c.Value, c => LegalNationalId.FromString(c))
        {

        }
    }
}
