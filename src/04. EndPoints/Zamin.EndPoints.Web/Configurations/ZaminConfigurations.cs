using System;
using System.Linq;

namespace Zamin.EndPoints.Web.Configurations
{


    public class ZaminConfigurations
    {
        public string ServiceId { get; set; } = "Service01";
        public string JsonSerializerTypeName { get; set; }

        public string ExcelSerializerTypeName { get; set; }
        public string UserInfoServiceTypeName { get; set; }
        public bool RegisterAutomapperProfiles { get; set; } = true;
        public string AssmblyNameForLoad { get; set; }
        public Messaging Messaging { get; set; }

        public Translator Translator { get; set; }

        public Swagger Swagger { get; set; }
        public Chaching Chaching { get; set; }
    }
}
