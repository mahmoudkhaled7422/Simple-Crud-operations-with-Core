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
    public class DepartmentCoursesController : Controller
    {
        private readonly DBModel _context;

        public DepartmentCoursesController(DBModel context)
        {
            _context = context;
        }

        // GET: DepartmentCourses
        public async Task<IActionResult> Index()
        {
            var dBModel = _context.DepartmentCourses.Include(d => d.Course).Include(d => d.Department);
            return View(await dBModel.ToListAsync());
        }

        // GET: DepartmentCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentCourse = await _context.DepartmentCourses
                .Include(d => d.Course)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.deptId == id);
            if (departmentCourse == null)
            {
                return NotFound();
            }

            return View(departmentCourse);
        }

        // GET: DepartmentCourses/Create
        public IActionResult Create()
        {
            ViewData["crsId"] = new SelectList(_context.Courses, "crsId", "crsName");
            ViewData["deptId"] = new SelectList(_context.Departments, "deptId", "deptName");
            return View();
        }

        // POST: DepartmentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("deptId,crsId")] DepartmentCourse departmentCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["crsId"] = new SelectList(_context.Courses, "crsId", "crsName", departmentCourse.crsId);
            ViewData["deptId"] = new SelectList(_context.Departments, "deptId", "deptName", departmentCourse.deptId);
            return View(departmentCourse);
        }

        // GET: DepartmentCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentCourse = await _context.DepartmentCourses.FindAsync(id);
            if (departmentCourse == null)
            {
                return NotFound();
            }
            ViewData["crsId"] = new SelectList(_context.Courses, "crsId", "crsName", departmentCourse.crsId);
            ViewData["deptId"] = new SelectList(_context.Departments, "deptId", "deptName", departmentCourse.deptId);
            return View(departmentCourse);
        }

        // POST: DepartmentCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("deptId,crsId")] DepartmentCourse departmentCourse)
        {
            if (id != departmentCourse.deptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentCourseExists(departmentCourse.deptId))
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
            ViewData["crsId"] = new SelectList(_context.Courses, "crsId", "crsName", departmentCourse.crsId);
            ViewData["deptId"] = new SelectList(_context.Departments, "deptId", "deptName", departmentCourse.deptId);
            return View(departmentCourse);
        }

        // GET: DepartmentCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentCourse = await _context.DepartmentCourses
                .Include(d => d.Course)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.deptId == id);
            if (departmentCourse == null)
            {
                return NotFound();
            }

            return View(departmentCourse);
        }

        // POST: DepartmentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentCourse = await _context.DepartmentCourses.FindAsync(id);
            _context.DepartmentCourses.Remove(departmentCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentCourseExists(int id)
        {
            return _context.DepartmentCourses.Any(e => e.deptId == id);
        }
    }
}
