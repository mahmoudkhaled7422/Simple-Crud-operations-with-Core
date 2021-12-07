using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using day1.Models;

namespace day1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DBModel _context;

        public DepartmentsController(DBModel context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.deptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("deptId,deptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("deptId,deptName")] Department department)
        {
            if (id != department.deptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.deptId))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.deptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult AddCourse(int id)
        {
            var allCourses = _context.Courses.ToList();
            var CoureseIn = _context.DepartmentCourses.Where(a => a.deptId == id).Select(a => a.Course);
            var CourseNotIn = allCourses.Except(CoureseIn).ToList();
            ViewBag.dept = _context.Departments.FirstOrDefault(a => a.deptId == id);
            return View(CourseNotIn);
        }
        [HttpPost]
        public ActionResult AddCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (KeyValuePair<string, bool> item in crs)
            {
                if (item.Value == true)
                {
                    _context.DepartmentCourses.Add(new DepartmentCourse { deptId = id, crsId = int.Parse(item.Key) });
                }
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult RemoveCourse(int id)
        {
            var CoureseIn = _context.DepartmentCourses.Where(a => a.deptId == id).Select(a => a.Course).ToList();
            ViewBag.dept = _context.Departments.FirstOrDefault(a => a.deptId == id);
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
                    var courseIn = _context.DepartmentCourses.FirstOrDefault(a => a.deptId == id && a.crsId == key);
                    _context.DepartmentCourses.Remove(courseIn);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.deptId == id);
        }
    }
}
