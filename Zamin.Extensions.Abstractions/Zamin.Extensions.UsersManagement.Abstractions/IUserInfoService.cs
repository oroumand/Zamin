﻿namespace Zamin.Extentions.UsersManagement.Abstractions;

public interface IUserInfoService
{
    string GetUserAgent();

    string GetUserIp();

    int UserId();

    string GetFirstName();

    string GetLastName();

    string GetUsername();

    bool IsCurrentUser(string userId);

    bool HasAccess(string accessKey);
}