namespace Zamin.Extensions.UsersManagement.Options;
public sealed class UserManagementOptions
{
    public string DefaultUserId { get; set; } = "1";
    public string DefaultUserAgent { get; set; } = "Unknown";
    public string DefaultUserIp { get; set; } = "0.0.0.0";
    public string DefaultUsername { get; set; } = "UnknownUserName";
    public string DefaultFirstName { get; set; } = "UnknownFirstName";
    public string DefaultLastName { get; set; } = "UnknownLastName";
}
