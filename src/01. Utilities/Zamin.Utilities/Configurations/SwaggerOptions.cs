namespace Zamin.Utilities.Configurations;
public class SwaggerOptions
{
    public bool Enabled { get; set; } = true;
    public SwaggerdocOptions SwaggerDoc { get; set; } = new SwaggerdocOptions();
}

public class SwaggerdocOptions
{
    public string Version { get; set; } = "v1";
    public string Title { get; set; } = "My Application Title";
    public string Name { get; set; } = "v1";
    public string URL { get; set; } = "/swagger/v1/swagger.json";
}
