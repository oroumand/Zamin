using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common.ValueConverters
{
    public class TitleValueConverter : ValueConverter<Title, string>
    {
        public TitleValueConverter(ConverterMappingHints mappingHints = null)
            : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static Expression<Func<Title, string>> convertToProviderExpression = title => title.Value;

        private static Expression<Func<string, Title>> convertFromProviderExpression = stringTitle => Title.FromString(stringTitle);
    }
}
