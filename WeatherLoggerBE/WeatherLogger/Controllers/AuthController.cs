using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthController : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/"
        }, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        return SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
