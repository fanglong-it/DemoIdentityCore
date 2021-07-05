using DemoIdentityCore.Areas.Identity.Data;
using DemoIdentityCore.Areas.Identity.Pages.Account;
using DemoIdentityCore.Data;
using DemoIdentityCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoIdentityCore.Controllers
{

    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {

        private readonly DemoIdentityCoreContext _db;
        private readonly UserManager<DemoIdentityCoreUser> _userManager;
    
        public AccountController(DemoIdentityCoreContext db, UserManager<DemoIdentityCoreUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        // GET: AccountController
        public ActionResult Index()
        {
            var listUser = _db.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                FirstName = x.Firstname,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            }).ToList();
            return View(listUser);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(string id)
        {
            UserViewModel user_detail = _db.Users.Where(x => x.Id == id).
                Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FirstName = x.Firstname,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }).FirstOrDefault();
            return View(user_detail);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                var user = new DemoIdentityCoreUser {
                    Firstname = collection["FirstName"],
                    LastName = collection["LastName"],

                    UserName = collection["Email"],
                    NormalizedUserName = collection["Email"],
                    Email = collection["Email"],
                    NormalizedEmail = collection["Email"],
                    
                    PhoneNumber = collection["PhoneNumber"],
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var result = await _userManager.CreateAsync(user, "Abc**123");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(string id)
        {

            UserViewModel user_detail = _db.Users.Where(x => x.Id == id).
                Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FirstName = x.Firstname,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }).FirstOrDefault();

            return View(user_detail);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, IFormCollection collection)
        {
            try
            {
                var user = _db.Users.Where(x => x.Id == id).FirstOrDefault();
                user.UserName = collection["UserName"];
                user.Firstname = collection["FirstName"];
                user.LastName = collection["LastName"];
                user.PhoneNumber = collection["PhoneNumber"];

                var result = await _userManager.UpdateAsync(user);



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(string id)
        {
            UserViewModel user_detail = _db.Users.Where(x => x.Id == id).
                Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FirstName = x.Firstname,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }).FirstOrDefault();

            return View(user_detail);
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id, IFormCollection collection)
        {
            try
            {
                var user = _db.Users.Where(x => x.Id == id).FirstOrDefault();
                var result = await _userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
