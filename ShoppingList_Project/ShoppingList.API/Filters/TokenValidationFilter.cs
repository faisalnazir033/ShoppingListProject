using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingList.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace ShoppingList.API.Filters;
public class TokenValidationFilter(IUserService userService) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!ValidateToken(token).Result)
        {
            context.Result = new UnauthorizedObjectResult("Invalid or expired token");
            return;
        }

        base.OnActionExecuting(context);
    }

    private async Task<bool> ValidateToken(string token)
    {
        try
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var userId = jwtToken.Claims.First(c => c.Type.ToLower() == "sub")?.Value;
            var userEmail = jwtToken.Claims.First(c => c.Type.ToLower() == "email").Value;

            var userInfo = await userService.GetUserByEmailAsync(userEmail);

            if (userEmail.Equals(userInfo.Email) && userId.ToString().Equals(userInfo.Id.ToString()))            
                return true;            

            return false;
        }
        catch (Exception)
        {
            return false;
        }        
    }
}