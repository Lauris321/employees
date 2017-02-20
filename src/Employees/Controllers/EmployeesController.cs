using Employees.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: /<controller>/
        public IActionResult List(int? id)
        {
            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;

            ViewData["page"] = id;
            ViewData["prevPage"] = id - 1;
            ViewData["nextPage"] = id + 1;
            ViewData["search"] = "";

            return View("List", context.GetSomeEmployees(id));
        }

        public IActionResult Search(string searchString)
        {
            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;
            ViewData["search"] = searchString;
            ViewData["page"] = 0;
            ViewData["prevPage"] = -1;
            ViewData["nextPage"] = 1;

            return View("List", context.Search(searchString));
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name,LastName,NetWage")] Employee employee)
        {
            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;
            context.Create(employee);
            ViewData["search"] = "";
            ViewData["page"] = 0;
            ViewData["prevPage"] = -1;
            ViewData["nextPage"] = 1;
            return View("List", context.GetAllEmployees());
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;

            return View(context.GetEmployee(id));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;

            return View(context.GetEmployee(id));
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name,LastName,NetWage")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;
                context.Edit(employee);
            }
            return View("Details", employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;

            return View(context.GetEmployee(id));
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;
            if (ModelState.IsValid)
            {
                context.Delete(id);
                ViewData["search"] = "";
                ViewData["page"] = 0;
                ViewData["prevPage"] = -1;
                ViewData["nextPage"] = 1;
            }
            return View("List", context.GetSomeEmployees(0));
        }
    }
}
