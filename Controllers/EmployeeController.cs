using ADOPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ADOPractice.Controllers
{
    public class EmployeeController : Controller
    {
       EmployeeActions employeeActions = new EmployeeActions();
        public IActionResult Index()
        {
           List<Employee> employees = new List<Employee>();
           employees= employeeActions.GetAllEmsployees().ToList();
           return View(employees);
        }

        public IActionResult Details(int id)
        {
            var list = employeeActions.GetEmployeeById(id);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            //List<Employee> employees = new List<Employee>();
            employeeActions.AddEmployee(emp);
            return RedirectToAction("Index");
            
        }

        public IActionResult Edit(int id)
        {
            Employee emp = employeeActions.GetEmployeeById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {   
            employeeActions.UpdateEmployee(emp);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            employeeActions.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
