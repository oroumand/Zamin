using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.EndPoints.Web.Configurations
{
    public class Swagger
    {
        public Swaggerdoc SwaggerDoc { get; set; }
    }

    public class Swaggerdoc
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
