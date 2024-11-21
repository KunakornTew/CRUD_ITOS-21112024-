using Microsoft.AspNetCore.Mvc;
using ITOS_LEARN.Models;
using ITOS_LEARN.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ITOS_LEARN.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult UserList()
        {
            var users = _context.Users.Where(u => u.Role == "User" && !u.IsConfirmed).ToList();
            return View(users);
        }

        public IActionResult ConfirmUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.User_ID == id);
            if (user != null)
            {
                user.IsConfirmed = true;
                _context.SaveChanges();
            }
            return RedirectToAction("UserList");
        }
    }
}
