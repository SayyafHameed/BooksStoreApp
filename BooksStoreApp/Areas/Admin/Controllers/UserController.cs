using BooksStore.DataAccess.Data;
using BooksStore.DataAccess.Repository.IRepository;
using BooksStore.Models;
using BooksStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace BooksStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            return View(_context.applicationUsers.Where(x => x.Id != userId).ToList());
        }

        public IActionResult LockUnlock([FromBody]string userId)
        {
            var user = _context.applicationUsers.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
    }
}
