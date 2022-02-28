using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Utilities.Configurations;
public class AppPartOptions
{
    public bool Enabled { get; set; } = true;
    public bool AutoCreateSqlTable { get; set; } = true;
    public string ApplicationTableName { get; set; } = "Applications";
    public string ModuleTableName { get; set; } = "Modules";
    public string ServiceTableName { get; set; } = "Services";
    public string ControllerTableName { get; set; } = "Controllers";
    public string ActionTableName { get; set; } = "Actions";
    public string SchemaName { get; set; } = "dbo";
    public string ConnectionString { get; set; }
}
