namespace Zamin.Toolkits.Services.Users
{
    public interface IUserInfoService
    {
        string GetUserAgent();
        string GetUserIp();
        int UserId();
    }
}

