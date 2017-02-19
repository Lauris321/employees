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

            return View("Index", context.GetSomeEmployees(id));
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

            return View("Index", context.GetAllEmployees());
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            EmployeeContext context = HttpContext.RequestServices.GetService(typeof(Employees.Models.EmployeeContext)) as EmployeeContext;

            return View(context.GetEmployee(id));
        }
    }
}
