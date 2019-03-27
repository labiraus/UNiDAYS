using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UNiDAYS.Identity.Models;

namespace UNiDAYS.Identity.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IEmailSender _emailSender;

        public IdentityController(UserManager<UserModel> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        // GET: Identity
        public IActionResult Create()
        {
            return View();
        }

        // POST: Identity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user == null)
                    {
                        user = new UserModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = model.UserName
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                            return View("Success");
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                    }
                    else
                    {
                        //Email the user to tell them someone tried to create an account in their name
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

    }
}