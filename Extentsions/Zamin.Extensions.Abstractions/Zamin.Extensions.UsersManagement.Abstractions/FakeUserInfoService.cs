namespace Zamin.Extentions.UsersManagement.Abstractions;

public class FakeUserInfoService : IUserInfoService
{
    public string GetFirstName()
    {
        return "FirstName";
    }

    public string GetLastName()
    {
        return "LastName";
    }

    public string GetUserAgent()
    {
        return "1";
    }

    public string GetUserIp()
    {
        return "0.0.0.0";
    }

    public string GetUsername()
    {
        return "Username";
    }

    public bool HasAccess(string access)
    {
        return true;
    }

    public bool IsCurrentUser(string userId)
    {
        return true;
    }

    public string UserId()
    {
        return "1";
    }
}
