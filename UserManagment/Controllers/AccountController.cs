using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserManagment.Models;
using UserManagment.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagment.Domain;
using UserManagment.Models.ViewComponents;
using UserManagment.Domain.Repositories.EntityFramework;
using System.Net;

namespace UserManagment.Controllers
{
    [Authorize]// - для данной области на сайте действует правило авторизации
    public class AccountController : Controller
    {
        private AppDbContext db;
        private readonly UserManager<UserData> userManager;
        private readonly SignInManager<UserData> signInManager;


        public AccountController(AppDbContext _db, UserManager<UserData> userMng, SignInManager<UserData> signinMng)
        {
            userManager = userMng;
            signInManager = signinMng;
            db = _db;
        }

       [AllowAnonymous] //-для данного действие необходимо быть не авторизованным
       //передача в качестве модели в представление логин
        public IActionResult Login(string returnUrl) //-действие Login
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {

                UserData user = await userManager.FindByNameAsync(model.UserName);
                //UserData user = db.Users.FirstOrDefault(user => user.UserName == model.UserName);
               
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if(result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                    if (!user.EmailConfirmed)
                        ModelState.AddModelError(nameof(LoginViewModel.UserName), "Необходимо активировать учетную запись перейдя по ссылке, отправленной в письме");
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
                
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
           
            PasswordViewModel model = new PasswordViewModel();
            model.UserId = Request.Query["userId"];
            model.Code = Request.Query["code"];
      
            return View(model);

        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(PasswordViewModel model)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == model.UserId);
            string code = WebUtility.UrlDecode(model.Code);
            code = code.Replace(" ", "+");
            var result= await userManager.ResetPasswordAsync(user, code, model.Password);
            if(!result.Succeeded)
            {
                return RedirectToAction("ResetPassword", "Account");
            }

           // user.PasswordHash = new PasswordHasher<UserData>().HashPassword(null, model.Password);
            user.EmailConfirmed = true;

            db.Users.Update(user);
            db.SaveChanges();



            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }



    }
}
