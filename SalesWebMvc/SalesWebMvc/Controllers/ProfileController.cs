using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Select()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Set(UserProfile profile)
        {
            var perfilAtual = HttpContext.Session.GetInt32("UserProfile") ?? 0;

            if ((int)profile == perfilAtual)
            {
                ViewData["Message"] = "Você já está usando esse perfil!";
                ViewData["CurrentProfile"] = profile;
                return View("Change");
            }

            HttpContext.Session.SetInt32("UserProfile", (int)profile);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Change()
        {
            var perfilInt = HttpContext.Session.GetInt32("UserProfile") ?? 0;
            var perfil = (UserProfile)perfilInt;

            ViewData["CurrentProfile"] = perfil;

            return View();
        }
    }
}
