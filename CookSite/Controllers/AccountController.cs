using CookSite.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookSite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Girilen verilerin yapısı doğru değil!");
                }

                if (data.Username=="abc" && data.Password=="1234") {
                    //oturum açma kodları buraya gelecek
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, data.Username),
                        new Claim("Id","10")
                    };

                    var authenticationProps= new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    };

                    var claimsIdentity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                 await   HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProps);

                    return RedirectToAction("Index", "Home");


                }
                else
                {
                    throw new Exception("Kullanıcı adı veya şifre hatalı!");
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;  

                return View(data);
            }

        }




    }
}
