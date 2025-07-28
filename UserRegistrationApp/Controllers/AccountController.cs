using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UserRegistrationApp.Data;
using UserRegistrationApp.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace UserRegistrationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            ViewBag.HideLayout = true;
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            ViewBag.HideLayout = true;

            // Email must end with @gmail.com
            if (!user.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Email", "Email must end with @gmail.com");
            }

            // Check if email already exists
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }

            // Validate DOB (Age > 18)
            var today = DateTime.Today;
            var age = today.Year - user.DOB.Year;
            if (user.DOB.Date > today.AddYears(-age)) age--;

            if (age < 18)
            {
                ModelState.AddModelError("DOB", "Age must be at least 18.");
            }

            if (ModelState.IsValid)
            {
                user.IsDisabled = false;
                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Login
        public IActionResult Login()
        {
            ViewBag.HideLayout = true;
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            ViewBag.HideLayout = true;

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                TempData["Toasts"] = "Invalid email or password.";
                return View();
            }

            if (user.IsDisabled)
            {
                TempData["Toasts"] = "Your account has been disabled.";
                return View();
            }

            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction("Index");
        }

        // GET: Logout
        public IActionResult Logout()
        {
            TempData["LogoutMessage"] = "Logged out successfully.";
            return RedirectToAction("Login");
        }

        // GET: User list
        // Add at the top
        public IActionResult Index(string search, int? page)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                users = users.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search) ||
                    u.Gender.ToLower().Contains(search));
            }

            ViewBag.Search = search;

            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(users.ToPagedList(pageNumber, pageSize));
        }


        // GET: Edit user
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Edit user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                _context.Update(updatedUser);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("Index");
            }

            return View(updatedUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(string email)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { success = false, message = "Email is required." });

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return Json(new { success = false, message = "User not found." });

            user.IsDisabled = !user.IsDisabled;
            _context.SaveChanges();

            return Json(new { success = true, isDisabled = user.IsDisabled });
        }


    }
}
