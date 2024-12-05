 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Students.Data;
using Students.Data.Models;
using Students.Models;

namespace Students.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext db;
        public StudentController(ApplicationDbContext db)
        {
            this.db = db;
        }   
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(StudentViewModel model)
        {
            Student student = new Student()
            {
                Name = model.Name,
                Phone = model.Phone,
                Address = model.Address
            };
            db.Students.Add(student);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
        public IActionResult All() 
        {
            List<StudentViewModel> model = db.Students.Select(x => new StudentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Address = x.Address
            }).ToList();
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.Id == id);
            db.Students.Remove(student);
            db.SaveChanges();
            return Redirect("/Student/All");
        }
    }
}
