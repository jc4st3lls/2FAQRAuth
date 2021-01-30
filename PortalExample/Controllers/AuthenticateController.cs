using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using PortalExample.Models;
using PortalExample.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalExample.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private static readonly SecurityKey SigningKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
        private static readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();
        public static readonly SigningCredentials SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
        public const string Issuer = "PortalExampleJwt";
        public const string Audience = "PortalExampleJwt";

        IHubContext<QRLogin> _hub;


        public AuthenticateController(IHubContext<QRLogin> hub)
        {
            _hub = hub;
        }

        

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QRAuthToken value)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.Code) && !string.IsNullOrEmpty(value.Uid))
                {
                    //var user = userService.GetUserBy(name);

                    //if (user == null)
                    //{
                    //    return Unauthorized();
                    //}

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, value.Uid),
                        new Claim(ClaimTypes.NameIdentifier, value.Uid)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);




                }
            }


            return NotFound();

        }

        
    }
}
