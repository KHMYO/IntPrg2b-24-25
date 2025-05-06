using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(opt =>
                            {
                                opt.LoginPath = "/Account/Login";
                                opt.LogoutPath = "/Account/Logout";
                                opt.AccessDeniedPath = "/Account/AccessDenied";
                                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                                opt.SlidingExpiration = true;
                                opt.Cookie.Name = "UserData";
                                opt.Cookie.HttpOnly = true;
                                opt.Cookie.SameSite = SameSiteMode.Strict;
                            });









            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //tek rota (route) kullanýlacaksa bu kalmalý
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=contact}/{id?}");





            //biden fazla rota kullanýlacaksa bu þekilde yazýlmalý
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                       name: "iletisim",
                       pattern: "iletisim",
                       defaults: new { controller = "Home", action = "Contact" }
                    );


                endpoints.MapControllerRoute(
                    name: "yemek",
                    pattern: "Yemek/{numara:int}",
                    defaults: new { controller = "Cook", action = "Detail" }
                    );

                endpoints.MapControllerRoute(
                    name: "yemek2",
                    pattern: "Yemek/{ad:alpha}",
                    defaults: new { controller = "Cook", action = "Detail" }
                    );

                endpoints.MapControllerRoute(
                    name: "sabit-anasayfa",
                    pattern: "anasayfa",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                   name: "Tarif",
                   pattern: "Tarif/{tip}/{no}",
                   defaults: new { controller = "Cook", action = "Detail2" }
                   );

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

            });


            app.Run();
        }
    }
}
