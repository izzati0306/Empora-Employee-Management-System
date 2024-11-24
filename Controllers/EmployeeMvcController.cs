using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorksAway.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WorksAway.Controllers
{
    public class EmployeeMvcController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeMvcController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeMvc
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View("Index", employees); // Returns the Index view inside the Views/EmployeeMvc folder
        }

        // GET: EmployeeMvc/Create
        public IActionResult Create()
        {
            return View(); // Returns the Create view inside the Views/EmployeeMvc folder
        }

        // POST: EmployeeMvc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the Index action after creating
            }
            return View(employee);
        }

        // GET: EmployeeMvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return 404 if the ID is null
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(); // Return 404 if the employee is not found
            }
            return View(employee); // Returns the Edit view inside the Views/EmployeeMvc folder
        }

        // POST: EmployeeMvc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound(); // Return 404 if ID doesn't match
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any(e => e.EmployeeID == id))
                    {
                        return NotFound(); // Return 404 if the employee is not found during update
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to Index after successful update
            }
            return View(employee);
        }

        // GET: EmployeeMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return 404 if the ID is null
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound(); // Return 404 if the employee is not found
            }

            return View(employee); // Returns the Delete view inside the Views/EmployeeMvc folder
        }

        // POST: EmployeeMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirect to Index after deletion
        }
    }
}