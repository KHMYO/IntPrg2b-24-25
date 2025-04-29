using CookSite.Models;
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
        private readonly CookSiteContext context;

        public AccountController()
        {
            context = new CookSiteContext();
        }


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

                var user= context
                            .Users
                            .FirstOrDefault(x=>x.Username==data.Username && x.Password==data.Password);


                if (user is not null) {
                    //oturum açma kodları buraya gelecek
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("Fullname",$"{user.Name} {user.Lastname}"),
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }



    }
}
