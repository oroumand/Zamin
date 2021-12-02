using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common.ValueConverters
{
    public class BusinessIdValueConverter : ValueConverter<BusinessId, Guid>
    {
        public BusinessIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static Expression<Func<BusinessId, Guid>> convertToProviderExpression = businessId => businessId.Value;

        private static Expression<Func<Guid, BusinessId>> convertFromProviderExpression = stringBusinessId => BusinessId.FromGuid(stringBusinessId);
    }
}
