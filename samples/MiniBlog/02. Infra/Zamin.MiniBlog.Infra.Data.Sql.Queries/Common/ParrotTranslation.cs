using System;
using System.Collections.Generic;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common
{
    public partial class ParrotTranslation
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Culture { get; set; }
    }
}
