namespace Zamin.Utilities.Services.Users
{
    public class FakeUserInfoService : IUserInfoService
    {
        public string GetUserAgent()
        {
            return "1";
        }

        public string GetUserIp()
        {
            return "1";
        }

        public int UserId()
        {
            return 1;
        }
    }
}
