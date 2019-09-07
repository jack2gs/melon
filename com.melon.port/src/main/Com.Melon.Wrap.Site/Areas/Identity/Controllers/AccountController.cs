using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Com.Melon.Wrap.Site.Areas.Users.Models;
using Com.Melon.Wrap.Site.Models;
using MediatR;
using Com.Melon.IdentityAccess.Application;
using Com.Melon.Wrap.Site.Areas.Identity.Models;
using Com.Melon.IdentityAccess.Domain;
using Com.Melon.Wrap.Site.Core.Application;
using Com.Melon.Wrap.Site.Core.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Com.Melon.Wrap.Site.Areas.Users.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Identity/Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // Post: Identity/Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel loginViewModel, string returnURL)
        {
            if(ModelState.IsValid){
                try
                {
                    await _mediator.Send(new RegisterUserCommand(loginViewModel.Email, loginViewModel.Password));
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                catch(InvalidOperationException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return View(loginViewModel);
        }

        // GET: Identity/Account/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            //string targetReturnURL = returnURL;
            //if (string.IsNullOrEmpty(targetReturnURL))
            //{
            //    targetReturnURL = Url.Action("Index", "Home");
            //}
            //ViewBag.ReturnURL = returnURL;
            return View();
        }

        // Post: Identity/Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnURL)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User authenticatedUser =  await _mediator.Send(new LoginCommand(loginViewModel.Email, loginViewModel.Password));

                    if(authenticatedUser == null)
                    {
                        throw new ArgumentException("User doesn't exist.");
                    }

                    Session session = await _mediator.Send(new CreateSessionCommand(authenticatedUser.Id));

                    Response.Cookies.Append("SessionID", session.SessionToken);

                    string redirectURL = returnURL;
                    if (string.IsNullOrEmpty(redirectURL))
                    {
                        redirectURL = Url.Action("Index", "Home");
                    }

                    return Redirect(redirectURL);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                catch (InvalidOperationException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return View(loginViewModel);
        }
    }
}
