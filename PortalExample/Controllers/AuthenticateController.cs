using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PortalExample.Models;
using PortalExample.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalExample.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        ISimpleCypher _cypher;
        IHubContext<QRLogin> _hub;
        IUserService _userService;

        public AuthenticateController(IUserService userService,ISimpleCypher cypher,IHubContext<QRLogin> hub)
        {
            _hub = hub;
            _userService = userService;
            _cypher = cypher;
        }

        [HttpGet]
        public string[] Get()
        {
            return new[] { "Runs!!" }; // Per test
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QRAuthToken value)
        {
            //Aqui es podria posar un identificador de dispositiu amb la App instal.lada.


            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.Code) && !string.IsNullOrEmpty(value.Uid))
                {

                    var client = _hub.Clients.Client(value.Code.Trim());

                    if (client != null)
                    {
                        var _uid = _cypher.RandomEncrypt(value.Uid);
                        await client.SendAsync("AuthenticateCode", _uid);
                        return Ok();
                    }
                    
                }
            }


            return NotFound();

        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] string uid)
        {
            var _uid = _cypher.RandomDecrypt(uid);

            var user = await _userService.GetUserBy(_uid);

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.Uid)
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = ""
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties);
            return Ok();

        }
    }
}
