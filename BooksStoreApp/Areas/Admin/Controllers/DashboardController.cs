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
    public class DashboardController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IActionResult Index()
        {
            ViewBag.Users = _unitOfWork.AppUser.GetAll().Count();
            ViewBag.Products = _unitOfWork.Product.GetAll().Count();
            return View();
        }
    }
}
