using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using LoginAndRegistration.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAndRegistration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpPost("register")]
        public IActionResult Register(Wrapper WrapperUser)
        {
            
            LRUser lruser=WrapperUser.UserForm;
            if(dbContext.LRUsers.Any(u => u.Email == lruser.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<LRUser> Hasher = new PasswordHasher<LRUser>();
                lruser.Password = Hasher.HashPassword(lruser, lruser.Password);
                dbContext.LRUsers.Add(lruser);
                dbContext.SaveChanges();
                // LRUser userInDb = dbContext.LRUsers.FirstOrDefault(u => u.Email == lruser.Email); 
                HttpContext.Session.SetInt32("CurrentId", lruser.UserId);
                // Wrapper OneUserId = LRUser userInD
                return RedirectToAction("Success");
            }
            else    
            {
                return View("Index"); 
            }
        }


        [HttpGet("success")]
        public IActionResult Success()
        {
            int? UserId = HttpContext.Session.GetInt32("CurrentId");   
            if(UserId == null)  
            {
                return RedirectToAction("Index");
            }
            LRUser CurrentUser = dbContext.LRUsers.FirstOrDefault(User => User.UserId == (UserId));
            return View("Success", CurrentUser);
        }
        
        public IActionResult Login(Wrapper WrapperUser)
        {
            LRLogin user=WrapperUser.LoginForm;
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.LRUsers.FirstOrDefault(u => u.Email == user.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email Combination. Please complete registration.");
                    return View("Index");
                }
                // PasswordHasher<lrlogin> hasher = new PasswordHasher<LRLogin>()
                var hasher = new PasswordHasher<LRLogin>();
                var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Password or Email/Password Combination. Best head over to registration.");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("CurrentId", userInDb.UserId);
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            } 
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
