using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using thewall.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace thewall.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (id is null)
                return RedirectToAction("Index", "Login");
            User user = dbContext.users.FirstOrDefault(u => u.UserId == id);
            TempData["name"] = user.FirstName;
            TempData["id"] = user.UserId;
            
            ViewBag.messages = dbContext.messages
                .Include(u => u.User)
                .Include(m => m.Comments)
                .ThenInclude(c => c.User)
                .ToList();
            
            // Dashboard dash = new Dashboard{
            //     Messages = messages
            // };
            // foreach(var msg in messages)
            // {
            //     System.Console.WriteLine(msg.User.FirstName);
            //     foreach(var com in msg.Comments)
            //     {
            //         System.Console.WriteLine(com.User.FirstName);
            //     }
            // }
            return View("dashboard");
        }

        [HttpPost("addMsg")]
        public IActionResult AddMsg(Message msg){
            Console.WriteLine("MSG is ================" + msg.MessageText);
            int? id = HttpContext.Session.GetInt32("UserId");
            User user = dbContext.users.FirstOrDefault(u => u.UserId == id);
            TempData["name"] = user.FirstName;
            
            if(ModelState.IsValid)
            {
                msg.UserId = user.UserId;
                dbContext.messages.Add(msg);
                dbContext.SaveChanges();
                return RedirectToAction("GetDashboard", "Home");
            }
            else
            {
                Console.WriteLine("MODELSTATE INVALID ===============\n\n");
                return View("dashboard", "Home");
            }
        }

        [HttpPost("addComment")]
        public IActionResult AddComment(Comment comment){
            int? id = HttpContext.Session.GetInt32("UserId");
            User user = dbContext.users.FirstOrDefault(u => u.UserId == id);
            TempData["name"] = user.FirstName;
            
            if(ModelState.IsValid)
            {
                dbContext.comments.Add(comment);
                dbContext.SaveChanges();
                return RedirectToAction("GetDashboard", "Home");
            }
            else
            {
                Console.WriteLine("MODELSTATE INVALID ===============\n\n");
                return View("dashboard", "Home");
            }
        }
    }
}
