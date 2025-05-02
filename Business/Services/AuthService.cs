using Data.Entities;
using Business.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(SignInFormData signInForm);
    Task LogoutAsync();
    Task<bool> SignUpAsync(SignUpFormData signUpForm);
}

public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : IAuthService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public async Task<bool> LoginAsync(SignInFormData signInForm)
    {
        var result = await _signInManager.PasswordSignInAsync(signInForm.Email, signInForm.Password, false, false);

        return result.Succeeded;
    }

    public async Task<bool> SignUpAsync(SignUpFormData signUpForm)
    {
        var userEntity = new UserEntity
        {
            UserName = signUpForm.Email,
            Email = signUpForm.Email,
            FirstName = signUpForm.FirstName,
            LastName = signUpForm.LastName,

        };

        var result = await _userManager.CreateAsync(userEntity, signUpForm.Password);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

}
