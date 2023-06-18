using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Extensions.ChangeDataLog.Sql.Options
{
    public class ChangeDataLogSqlOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool AutoCreateSqlTable { get; set; } = true;
        public string EntityTableName { get; set; } = "EntityChageDataLogs";
        public string PropertyTableName { get; set; } = "PropertyChageDataLogs";
        public string SchemaName { get; set; } = "dbo";
    }
}
