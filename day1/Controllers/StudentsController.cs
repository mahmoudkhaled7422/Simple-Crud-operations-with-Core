using day1.Models;
using day1.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DBModel _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentsController(DBModel context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var dBModel = _context.Students.Include(s => s.department);
            return View(dBModel);
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public IActionResult Create()
        {
            ViewData["deptId"] = new SelectList(_context.Departments, "deptId", "deptName");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind(include: "Id,Name,Age,Img,deptId")] StudentService model)
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Student student = new Student
                {
                  //  Id = model.Id,
                    Name=model.Name,
                    Age=model.Age,
                    Img = uniqueFileName,
                    deptId=model.deptId
                };
               _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptNo"] = new SelectList(_context.Departments, "DeptId", "DeptName", model.deptId);
            return View();
        }

        private string UploadedFile(StudentViewModel model)
        {
            string uniqueFileName = null;

            if (model.Img != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Img.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DeptNo"] = new SelectList(_context.Departments, "DeptId", "DeptName", student.deptId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptNo"] = new SelectList(_context.Departments, "DeptId", "DeptName", student.deptId);
            return View(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult CoursesList(int id)
        {
            var stdCourses = _context.StudentCourses.Where(a => a.Id == id).ToList();
            ViewBag.std = _context.Students.FirstOrDefault(a => a.Id == id);
            return View(stdCourses);
        }
        public ActionResult AddCourse(int id)
        {
            var allCourses = _context.Courses.ToList();
            var CourseIn = _context.StudentCourses.Where(a => a.Id == id).Select(a => a.Course);
            var CourseNotIn = allCourses.Except(CourseIn).ToList();
            ViewBag.std = _context.Students.FirstOrDefault(a => a.Id == id);
            return View(CourseNotIn);
        }
        [HttpPost]
        public ActionResult AddCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (KeyValuePair<string, bool> item in crs)
            {
                if (item.Value == true)
                {
                    _context.StudentCourses.Add(new StudentCourse() { Id = id, crsId = int.Parse(item.Key) });
                }
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult RemoveCourse(int id)
        {
            var CoureseIn = _context.StudentCourses.Where(a => a.Id == id).Select(a => a.Course).ToList();
            ViewBag.std = _context.Students.FirstOrDefault(a => a.Id == id);
            return View(CoureseIn);
        }
        [HttpPost]
        public ActionResult RemoveCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (var item in crs)
            {
                if (item.Value == true)
                {
                    int key = int.Parse(item.Key);
                    var courseIn = _context.StudentCourses.FirstOrDefault(a => a.Id == id && a.crsId == key);
                    _context.StudentCourses.Remove(courseIn);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
