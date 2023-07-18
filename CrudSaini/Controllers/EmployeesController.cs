using CrudSaini.Data;
using CrudSaini.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudSaini.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DBcontext dbcontext;

        public EmployeesController(DBcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
           var emp=await dbcontext.Employees.ToListAsync();
            return View(emp);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Add(AddEmployeeViewModel addEmployee)
        {
            var employee=new Employee()
            {
                Id=Guid.NewGuid(),
                Name=addEmployee.Name,
                Email=addEmployee.Email,
                Salary=addEmployee.Salary,
                DateOfBirth=addEmployee.DateOfBirth,
                Department=addEmployee.Department,
            };
           await dbcontext.Employees.AddAsync(employee);
            await dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        //This is update method
        public async Task< IActionResult> View(Guid id)
        {
            
            var empl= await dbcontext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (empl != null) {
                var updateView = new UpdateEmployeeViewModel()
                {
                    Id = empl.Id,
                    Name = empl.Name,
                    Email = empl.Email,
                    Salary = empl.Salary,
                    DateOfBirth = empl.DateOfBirth,
                    Department = empl.Department,

                };
                return await Task.Run(()=> View("view",updateView));
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model) {

            var employee = await dbcontext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                employee.Salary = model.Salary;
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
               await dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee= await dbcontext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                dbcontext.Employees.Remove(employee);
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    } 
}
