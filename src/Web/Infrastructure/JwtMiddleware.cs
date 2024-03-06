using Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace Web.Infrastructure;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (context.Connection.LocalPort is 5151 or 5030)//GRPC Ports
        {
            await _next(context);
            return;
        }
        if (token == null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        if (!attachUserToContext(context, token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        await _next(context);
    }

    private bool attachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtTokenConfig.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var phoneNumber = jwtToken.Claims.First(x => x.Type == "phoneNumber").Value;
            var roles = jwtToken.Claims.First(x => x.Type == "roles").Value;
            var userId = jwtToken.Claims.First(x => x.Type == "useID").Value;
            List<string> rolesList = roles.Split(',').ToList();                // attach user to context on successful jwt validation

            // attach user to context on successful jwt validation
            context.Items["UserId"] = userId;
            return true;
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
            return false;
        }
    }
}
