using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.Models;
using UserManagment.Service;

namespace UserManagment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(EmailViewModel emailViewModel, [FromServices] IEmailSender emailSender)
        {
            emailSender.Send(emailViewModel.Email, emailViewModel.Text, emailViewModel.Subject);
            return View();
        }
        
    }
}
