using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using shop.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.Json;

namespace shop.Controllers
{
    public class HomeController : Controller
    {

        private SportShopContext db;
        public HomeController(SportShopContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }
            
        public async Task<IActionResult> Ball()
        {
            return View(await db.Products.ToListAsync());
         
        }
        public async Task<IActionResult> Shorts()
        {
            return View(await db.Products.ToListAsync());
            //return View();
        }
     
        public async Task <IActionResult> Sneakers()
        {
            return View(await db.Products.ToListAsync());
        }
        public IActionResult Basket()
        {
            Basket basket = new Basket();

            if (HttpContext.Session.Keys.Contains("Basket"))
                basket = JsonSerializer.Deserialize<Basket>(HttpContext.Session.GetString("Basket"));

            return View(basket);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(User person)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(person);
                db.Users.Add(person);
                await db.SaveChangesAsync();
                return RedirectToAction("SignUp");
            }
            else
            {

                return View(person);
            }
        }
        public IActionResult SignIn()
        {
            if (HttpContext.Session.Keys.Contains("AuthUser"))
            {
                return RedirectToAction("Index", "Home");
               
               
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.LoginUser == model.Login && u.PasswordUser == model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("AuthUser", model.Login);
                    await Authenticate(model.Login); 
                    return RedirectToAction("Index", "Home");
                    
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return Content("Неверный логин и/или пароль");




            }
            

            return RedirectToAction("SignIn", "Home");
        }
        private async Task Authenticate(string userName)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("SignIn");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddToCart()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);

            Basket basket = new Basket();

            if (HttpContext.Session.Keys.Contains("Basket"))

                basket = JsonSerializer.Deserialize<Basket>(HttpContext.Session.GetString("Basket"));

            basket.CartLines.Add(db.Products.Find(ID));

            HttpContext.Session.SetString("Basket", JsonSerializer.Serialize<Basket>(basket));

            return Redirect("~/Home/Index");

        }

        public IActionResult RemoveFromCart()
        {
            int number = Convert.ToInt32(Request.Query["Number"]);

            Basket basket = new Basket();

            if (HttpContext.Session.Keys.Contains("Basket"))

                basket = JsonSerializer.Deserialize<Basket>(HttpContext.Session.GetString("Basket"));

            basket.CartLines.RemoveAt(number);

            HttpContext.Session.SetString("Basket", JsonSerializer.Serialize<Basket>(basket));

            return Redirect("~/Home/Basket");

        }

        public IActionResult RemoveAllFromCart()
        {
            //int ID = Convert.ToInt32(Request.Query["ID"]);
          

            //Basket basket = new Basket();

            //if (HttpContext.Session.Keys.Contains("Basket"))
            //    basket = JsonSerializer.Deserialize<Basket>(HttpContext.Session.GetString("Basket"));
            //basket.CartLines.RemoveAll(item => item.IdProduct == ID);
           
            //HttpContext.Session.SetString("Basket", JsonSerializer.Serialize(basket));

            //return Redirect("~/Home/Basket");
             Basket basket = new Basket();

    if (HttpContext.Session.Keys.Contains("Basket"))
        basket = JsonSerializer.Deserialize<Basket>(HttpContext.Session.GetString("Basket"));

    basket.RemoveAllFromCart();

    HttpContext.Session.SetString("Basket", JsonSerializer.Serialize(basket));

    return Redirect("~/Home/Basket");

        }


      

   
    }
}