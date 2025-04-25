using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public async Task<SignInResult> SignInAsync(SignInFormData formData)
    {
        if(formData == null)
        {
            return new SignInResult();
        }

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, lockoutOnFailure: false);
        return result;
    }









    //public async Task SignUpAsync()



    //public async Task<IdentityResult> RegisterAsync(string email, string password, string firstName, string lastName)
    //{
    //    var user = new UserEntity
    //    {
    //        UserName = email,
    //        Email = email,
    //        FirstName = firstName,
    //        LastName = lastName
    //    };

    //    return await _userManager.CreateAsync(user, password);
    //}

    //public async Task<SignInResult> LoginAsync(string email, string password)
    //{
    //    return await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
    //}

    //public async Task LogoutAsync()
    //{
    //    await _signInManager.SignOutAsync();
    //}


}
