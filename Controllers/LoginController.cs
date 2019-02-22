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

namespace thewall.Controllers{
  public class LoginController: Controller{
    private MyContext dbContext;
    public LoginController(MyContext context)
    {
      dbContext = context;
    }

    [HttpGet("")]
    public IActionResult Index(){
      return View("Index");
    }

    [HttpPost("register")]
    public IActionResult Register(User user){
      if(ModelState.IsValid){
        User exists = dbContext.users.FirstOrDefault(u => u.Email == user.Email);

        if(exists != null){
          ModelState.AddModelError("Email", "An account with this email already exists");
          return View("Index");
        }
        else{
          PasswordHasher<User> Hasher = new PasswordHasher<User>();
          user.Password = Hasher.HashPassword(user, user.Password);
          dbContext.Add(user);
          //    Console.WriteLine("USER ADDED============= " + user.FirstName);
          dbContext.SaveChanges();
          HttpContext.Session.SetInt32("UserId", user.UserId);
          return RedirectToAction("GetDashboard", "Home", new {userId = user.UserId});
        }
      }
      else
        return View("Index");
    }

    [HttpGet("login")]
    public IActionResult Login(){
      return View("Login");
    }

    [HttpPost("processLogin")]
    public IActionResult ProcessLogin(LoginUser loginUser){
      if(ModelState.IsValid){
        User userInDb = dbContext.users.FirstOrDefault(u => u.Email == loginUser.Email);
        if(userInDb == null){
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Login");
        }
        var hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.Password);
        if(result == 0){
          ModelState.AddModelError("Password", "Incorrect Password");
        }
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        User user = dbContext.users.FirstOrDefault(u => u.UserId == userInDb.UserId);
        TempData["name"] = user.FirstName;
        
        return RedirectToAction("GetDashboard", "Home", new {userId = user.UserId});
      }
      else
        return View("Login");
    }

    [HttpGet("logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return View("Index");
    }
  }
}