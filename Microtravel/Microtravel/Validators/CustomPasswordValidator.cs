using Microsoft.AspNetCore.Identity;

namespace Microtravel.Validators
{
    public class CustomPasswordValidator : IPasswordValidator<IdentityUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            var errors = new List<IdentityError>();

            if (!password.Any(char.IsDigit))
                errors.Add(new IdentityError { Description = "A jelszónak tartalmaznia kell legalább egy számjegyet ('0'-'9')." });

            if (!password.Any(char.IsUpper))
                errors.Add(new IdentityError { Description = "A jelszónak tartalmaznia kell legalább egy nagybetűt ('A'-'Z')." });

            if (!password.Any(char.IsLower))
                errors.Add(new IdentityError { Description = "A jelszónak tartalmaznia kell legalább egy kisbetűt ('a'-'z')." });

            if (password.All(char.IsLetterOrDigit))
                errors.Add(new IdentityError { Description = "A jelszónak tartalmaznia kell legalább egy nem alfanumerikus karaktert ($, @, * stb.)." });

            return Task.FromResult(errors.Count == 0
                ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
