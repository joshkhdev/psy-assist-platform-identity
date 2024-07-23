using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;

namespace PsyAssistPlatform.AuthService.WebApi;
public class CustomTokenRequestValidator : ICustomTokenRequestValidator
{
    public Task ValidateAsync(CustomTokenRequestValidationContext context)
    {
        var request = context.Result.ValidatedRequest;

        if (request.GrantType == GrantType.ClientCredentials)
        {
            // Добавление пользовательских утверждений из параметров запроса
            var roleClaim = request.Raw.Get("role");
            if (!string.IsNullOrEmpty(roleClaim))
            {
                var clientClaims = new List<Claim>
                {
                    new Claim("role", roleClaim)
                };

                clientClaims.ForEach(request.ClientClaims.Add);
            }
        }

        return Task.CompletedTask;
    }
}

