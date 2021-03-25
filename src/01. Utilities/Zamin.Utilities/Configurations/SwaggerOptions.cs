namespace Zamin.Utilities.Configurations
{
    public class SwaggerOptions
    {
        public bool Enabled { get; set; } = true;
        public SwaggerdocOptions SwaggerDoc { get; set; }
    }

    public class SwaggerdocOptions
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
