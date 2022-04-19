using Core_Pro_For_Test.DB_Context;
using Core_Pro_For_Test.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core_Pro_For_Test.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index1()
        {
            ViewBag.id = 1;
            ViewBag.name = "Manish Tomar";
            ViewBag.email = "man@gmail.com";
            ViewBag.designation = "full stack developer";
            ViewBag.List = new List<string>()
            {
                "i",
                "love",
                "my",
                "my work"

            };
            ViewData["abc1"] = 2;
            ViewData["abc2"] = "Lavnish Tomar";
            ViewData["abc3"] = "lav@gmail.com";
            ViewData["abc4"] = "Meerut";
            ViewData["abc5"] = "Athlete";
            ViewData["abc"] = new List<string>()
            {
                "Ultramarathon Athlete"

            };
            TempData["abc"] = 3;
            TempData["abc1"] = "Vikas Tomar";
            TempData["abc2"] = "kali@gmail.com";
            TempData["abc3"] = "Athlete";
            TempData["abc4"] = 3000;
            TempData["er"] = new List<String>()
            {
               
                "3000 Meter Athlete",


            };
            HttpContext.Session.SetString("name", "Manish Tomar");
            var data = HttpContext.Session.GetString("name");

            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
           
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(userlogin mod)
        {
            Chetu_AdministrationContext obj = new Chetu_AdministrationContext();
            var user = obj.Logins.Where(m => m.Email == mod.Email).FirstOrDefault();
            if (user == null)
            {
                TempData["invalid"] = "Email.is not found invalid user ";
            }
            else
            {
                if(user.Email==mod.Email && user.Password == mod.Password)
                {
                    var claims = new[] {new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Name,user.Name) };
                    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authproperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        authproperties);

                    HttpContext.Session.SetString("name",user.Name );
                     HttpContext.Session.GetString("name");


                    return RedirectToAction("emp_list");

                }
                else
                {
                    TempData["not valid"] = "wrong password";
                }
            }



            return View();
        }
        public IActionResult emp_list()
        {
            List<Employee> set = new List<Employee>();
            Chetu_AdministrationContext obj = new Chetu_AdministrationContext();
            var res = obj.EmpInfos.ToList();

            foreach (var item in res)
            {
                set.Add(new Employee
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Department = item.Department,
                    Contact = item.Contact,
                    City = item.City,
                    Salary = item.Salary
                });
            }
            return View(set);

        }




        [AllowAnonymous]
        [HttpGet]
        public IActionResult Adduser()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Adduser(userlogin rock)
        {
            Chetu_AdministrationContext obj = new Chetu_AdministrationContext();
            Login set = new Login();
            set.Id= rock.Id;
            set.Name = rock.Name;
            set.Email = rock.Email;
            set.Password = rock.Password;
           
            
                obj.Logins.Add(set);
                obj.SaveChanges();
                return RedirectToAction("emp_list");

        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
      
        [HttpPost]
        public IActionResult Registration(Employee rock)
        {
            Chetu_AdministrationContext obj = new Chetu_AdministrationContext();
            EmpInfo set = new EmpInfo();
            set.Id = rock.Id;
            set.Name = rock.Name;
            set.Email = rock.Email;
            set.Department = rock.Department;
            set.Contact = rock.Contact;
            set.City = rock.City;
            set.Salary = rock.Salary;
            if (rock.Id == 0)
            {
                obj.EmpInfos.Add(set);
                obj.SaveChanges();
                return RedirectToAction("emp_list");
            }
            else
            {
                obj.Entry(set).State = EntityState.Modified;
                obj.SaveChanges();
                return RedirectToAction("emp_list");

            }



        }
        public IActionResult Edit(int id)
        {
            Chetu_AdministrationContext obj = new Chetu_AdministrationContext();
            var edit = obj.EmpInfos.Where(m => m.Id == id).First();
            Employee set = new Employee();
            set.Id = edit.Id;
            set.Name = edit.Name;
            set.Email = edit.Email;
            set.Department = edit.Department;
            set.Contact = edit.Contact;
            set.City = edit.City;
            set.Salary = edit.Salary;
            return View("Registration", set);
        }
        public IActionResult delete(int id)
        {
            Chetu_AdministrationContext obj = new Chetu_AdministrationContext();
            var dlt = obj.EmpInfos.Where(m => m.Id == id).First();
            obj.EmpInfos.Remove(dlt);
            obj.SaveChanges();
            return RedirectToAction("emp_list");
        }
        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");

        }
        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
