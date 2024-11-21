using Microsoft.AspNetCore.Mvc;
using ITOS_LEARN.Models;
using ITOS_LEARN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ITOS_LEARN.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            passwordHasher = new PasswordHasher<User>();
            _context = context;
        }

        // หน้า Login GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // หน้า Login POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ค้นหาผู้ใช้จากฐานข้อมูลตาม username และ email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Email == model.Email);

                if (user != null)
                {
                    // ตรวจสอบรหัสผ่าน
                    var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        // เก็บข้อมูล username, role และ isConfirmed ลงใน session
                        HttpContext.Session.SetString("User", user.Username);
                        HttpContext.Session.SetString("Role", user.Role); // เก็บบทบาทใน session
                        HttpContext.Session.SetString("IsConfirmed", user.IsConfirmed.ToString()); // เก็บสถานะการยืนยันใน session

                        // บันทึกข้อมูลการล็อกอิน
                        var login = new Login
                        {
                            Username = user.Username,
                            LoginTime = DateTime.Now,
                            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() // เก็บ IP Address
                        };

                        _context.Logins.Add(login); // เพิ่ม Login ลงในฐานข้อมูล
                        await _context.SaveChangesAsync(); // บันทึกการเปลี่ยนแปลง

                        // ตรวจสอบ Role และสถานะการยืนยัน
                        if (user.Role == "Admin")
                        {
                            // ถ้าเป็น Admin ให้ไปหน้า Detail เลย
                            return RedirectToAction("Index", "People");
                        }
                        else if (user.Role == "User" && user.IsConfirmed)
                        {
                            // ถ้าเป็น User และได้รับการยืนยันแล้ว ให้ไปหน้า Detail
                            return RedirectToAction("Index", "People");
                        }
                        else if (user.Role == "User" && !user.IsConfirmed)
                        {
                            // ถ้าเป็น User และยังไม่ได้รับการยืนยัน ให้ไปหน้า Waiting
                            return RedirectToAction("Index", "Waiting");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username, password, or email.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username, password, or email.");
                }
            }
            return View(model);
        }

        // หน้า Register GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var existingEmail = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                // เปลี่ยนจาก model เป็น user
                if (user.Role == "Admin")
                {
                    user.IsConfirmed = true;
                }
                else
                {
                    user.IsConfirmed = false;
                }

                // แฮชรหัสผ่าน
                var hashPassword = passwordHasher.HashPassword(user, user.Password);
                user.Password = hashPassword;

                // บันทึกข้อมูลผู้ใช้ใหม่
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // เก็บชื่อผู้ใช้จาก session ก่อน
            var username = HttpContext.Session.GetString("User");

            if (!string.IsNullOrEmpty(username))
            {
                // เพิ่ม LogoutTime ในฐานข้อมูล
                var login = await _context.Logins
                    .Where(l => l.Username == username && l.LogoutTime == null)  // ตรวจสอบว่า LogoutTime ยังเป็น null
                    .OrderByDescending(l => l.LoginTime)
                    .FirstOrDefaultAsync();

                if (login != null)
                {
                    login.LogoutTime = DateTime.Now;
                    await _context.SaveChangesAsync();  // บันทึกการเปลี่ยนแปลง
                }

                // ลบข้อมูลใน session ที่เก็บ user
                HttpContext.Session.Remove("User");
            }

            // พาผู้ใช้กลับไปหน้า Login
            return RedirectToAction("Login", "Account");
        }
    }
}
