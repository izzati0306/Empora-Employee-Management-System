using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorksAway.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WorksAway.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        
        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the admin exists in the database
                var admin = await _context.Admins
                                           .FirstOrDefaultAsync(a => a.Email == model.Email);
                
                if (admin != null && admin.Password == model.Password) // Compare the passwords (ensure they are hashed in a real-world scenario)
                {
                    // Simulate a successful login (you should store this in a session or JWT token for authentication)
                    TempData["AdminEmail"] = admin.Email; // Store admin email in TempData or use a session variable
                    return RedirectToAction("Index", "EmployeeMvc"); // Redirect to the Employees controller or wherever appropriate
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // For logging out (clear session or TempData)
        public IActionResult Logout()
        {
            TempData.Clear(); // Clear the stored admin email
            return RedirectToAction("Login");
        }
    }
}
