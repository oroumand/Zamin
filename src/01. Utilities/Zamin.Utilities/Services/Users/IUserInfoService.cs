namespace Zamin.Utilities.Services.Users
{
    public interface IUserInfoService
    {
        string GetUserAgent();
        string GetUserIp();
        int UserId();
    }
}

