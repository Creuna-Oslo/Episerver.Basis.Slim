using Creuna.Basis.Revisited.Web.Controllers.Base;
using Creuna.Basis.Revisited.Web.Models.ViewModels;
using System.Web.Mvc;
using EPiServer.Globalization;
using EPiServer.Web.Routing.Segments;
using Creuna.Basis.Revisited.Web.Business.UserHandling;
using EPiServer.Framework.Localization;

namespace Creuna.Basis.Revisited.Web.Controllers
{
    public class AccountController : LocalizedNonContentControllerBase
    {
        const string DefaultRedirectUrl = "/";

        IUserAuthenticationHandler UserAuthentication { get; }
        LocalizationService LocalizationService { get; }

        public AccountController(ILanguageSegmentMatcher languageSegmentMatcher, IUpdateCurrentLanguage updateCurrentLanguage, IUserAuthenticationHandler userAuthentication, LocalizationService localizationService) 
            : base(languageSegmentMatcher, updateCurrentLanguage)
        {
            UserAuthentication = userAuthentication;
            LocalizationService = localizationService;
        }

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToUrlOrDefault(returnUrl);

            SetLanguageFromUrl(returnUrl);
            return View(new LoginViewModel());
        }

        public ActionResult Logout(string returnUrl)
        {
            UserAuthentication.Logout();
            return RedirectToUrlOrDefault(returnUrl);
        }

        [HttpPost]
        public ActionResult Login(string returnUrl, LoginViewModel model)
        {
            SetLanguageFromUrl(returnUrl);

            if (!ModelState.IsValid)
                return View(model);

            var success = UserAuthentication.Login(model.Username, model.Password, model.PersistLogin);

            if (success)
                return RedirectToUrlOrDefault(returnUrl);

            ModelState.AddModelError(string.Empty, LocalizationService.GetString("/Account/LoginFailed"));

            return View(model);
        }

        RedirectResult RedirectToUrlOrDefault(string url)
            => Redirect(url ?? DefaultRedirectUrl);
    }
}