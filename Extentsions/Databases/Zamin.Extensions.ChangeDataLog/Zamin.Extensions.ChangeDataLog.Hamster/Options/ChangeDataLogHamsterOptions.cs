using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Extensions.ChangeDataLog.Sql.Options
{
    public class ChangeDataLogHamsterOptions
    {
        public List<string> propertyForReject { get; set; } = new List<string>
            {
                "CreatedByUserId",
                "CreatedDateTime",
                "ModifiedByUserId",
                "ModifiedDateTime"
            };
        public string BusinessIdFieldName { get; set; } = "BusinessId";

    }
}
