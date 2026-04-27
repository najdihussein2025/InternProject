namespace InternSystemProject.Helpers;

using Microsoft.AspNetCore.Http;
using InternSystemProject.Models;

public class SessionHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string KeyUserId = "UserId";
    private const string KeyUserRole = "UserRole";
    private const string KeyUserName = "UserName";

    public SessionHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session =>
        _httpContextAccessor.HttpContext!.Session;

    // Save minimal user data to session — never store password
    public void SetCurrentUser(User user)
    {
        Session.SetInt32(KeyUserId, user.Id);
        Session.SetString(KeyUserRole, user.Role);
        Session.SetString(KeyUserName, user.FullName);
    }


    public int? GetCurrentUserId()
    {
        return Session.GetInt32(KeyUserId);
    }


    public string? GetCurrentUserRole()
    {
        return Session.GetString(KeyUserRole);
    }


    public string? GetCurrentUserName()
    {
        return Session.GetString(KeyUserName);
    }


    public bool IsLoggedIn()
    {
        return Session.GetInt32(KeyUserId).HasValue;
    }

    public void Clear()
    {
        Session.Remove(KeyUserId);
        Session.Remove(KeyUserRole);
        Session.Remove(KeyUserName);
        Session.Clear();
    }
}