using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// declare user services
        /// </summary>
        private readonly IUserServices _userServices;
        
        /// <summary>
        /// declare login resouces
        /// </summary>
        private readonly ResourcesServices<LoginResource> _resourcesServices;
        
        /// <summary>
        /// declare logger
        /// </summary>
        private readonly ILogger<LoginController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="resourcesServices"></param>
        /// <param name="logger"></param>
        public LoginController(IUserServices userServices, ResourcesServices<LoginResource> resourcesServices, ILogger<LoginController> logger)
        {
            _userServices = userServices;
            _resourcesServices = resourcesServices;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestPath"></param>
        /// <returns>LoginForm</returns>
        public IActionResult LoginForm(string requestPath)
        {
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns>LoginForm</returns>
        [HttpPost]
        public async Task<IActionResult> LoginForm(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid) return View(loginViewModel);
            var isLogin = _userServices.Login(loginViewModel.Email, loginViewModel.PassWord, loginViewModel.IsActive);
            if (isLogin)
            {
                var user = await _userServices.GetEmail(loginViewModel.Email);
                var role = "";
                role = user.Role == 1 ? "Admin" : "User";
                var claims = new List<Claim>
                {
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Role, role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties()
                );
                return Redirect(loginViewModel.RequestPath ?? "/");
            }
            _logger.LogError("Login Fail",loginViewModel);
            ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_Login");
            return View(loginViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Logout</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginForm");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Setlanguage of Page</returns>
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {Expires = DateTimeOffset.UtcNow.AddDays(30)}
            );
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(culture);
            return LocalRedirect(returnUrl);
        }
    }
}