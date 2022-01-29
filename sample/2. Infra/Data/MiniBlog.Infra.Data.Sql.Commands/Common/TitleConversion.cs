using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class TitleConversion : ValueConverter<Title, string>
    {
        public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
        {

        }
    }
}
