using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagment.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserManagment.Domain.Repositories.EntityFramework;
using UserManagment.Domain.Entities;
using UserManagment.Controllers;
using System.Net.Mail;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Identity;
using UserManagment.Service;
using UserManagment.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace UserManagment.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {

        private AppDbContext db;
        private readonly UserManager<UserData> _userManager;
        private IUserDataRepository userDataRepository;

        public HomeController(AppDbContext context, UserManager<UserData> userManager, IUserDataRepository _userDataRepository)
        {
            userDataRepository = _userDataRepository;
            db = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //todo calculate isAdmin 
            return View(await userDataRepository.GetUserData().ToListAsync());
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            EFUserDataRepository userDataRepository = new EFUserDataRepository(db, _userManager);
            return View(userDataRepository.GetUserDataById(id));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserData user)
        {
            EFUserDataRepository userDataRepository = new EFUserDataRepository(db, _userManager);

            userDataRepository.SaveUserData(user);
            return RedirectToAction("Index", await userDataRepository.GetUserData().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            EFUserDataRepository userDataRepository = new EFUserDataRepository(db, _userManager);

            userDataRepository.DeleteUserData(id);
            return RedirectToAction("Index", await userDataRepository.GetUserData().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Register(string id)
        {
           return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserData user, [FromServices] IEmailSender emailSender)
        {
            EFUserDataRepository userDataRepository = new EFUserDataRepository(db, _userManager);

            userDataRepository.SaveUserData(user);

            user = userDataRepository.GetUserDataById(user.Id);
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var urlEncode = HttpUtility.UrlEncode(code);
            var callbackUrl = $"{Request.Scheme}://{Request.Host.Value}/Account/ResetPassword?userId={user.Id}&code={urlEncode}";

            string body = $"Подтвердите почту:{callbackUrl}";
            string subject = "Завершение регистрации";
            emailSender.Send(user.Email, body, subject);

            return RedirectToAction("Index", await userDataRepository.GetUserData().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ToggleAdmin(string id, [FromServices] UserManager<UserData> userManager, [FromServices] RoleManager<IdentityRole> roleManager)
        {
            var role = await roleManager.FindByNameAsync("admin");

            if (role == null)
                await roleManager.CreateAsync(new IdentityRole("admin"));

            var user = await userManager.FindByIdAsync(id);
            var isAdmin = await userManager.IsInRoleAsync(user, "admin");

            if(!isAdmin)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
            else
            {
                await userManager.RemoveFromRoleAsync(user, "admin");
            }
            return RedirectToAction("Index", await userDataRepository.GetUserData().ToListAsync());

        }




    }
}
