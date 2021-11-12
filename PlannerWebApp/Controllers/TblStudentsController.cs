using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerWebApp.Controllers
{
    public class TblStudentsController : Controller
    {
        private readonly PlannerContext _context;

        public TblStudentsController(PlannerContext context)
        {
            _context = context;
        }

        // GET: TblStudents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblStudents.ToListAsync());
        }

        // GET: TblStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblStudent tblStudent = await _context.TblStudents
                .FirstOrDefaultAsync(m => m.StudentNumber == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // GET: TblStudents/SemesterDetails
        public IActionResult OTPVerification()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OTPVerification([Bind("OneTimePin")] Verification verification)
        {
            if (ModelState.IsValid)
            {

                if (verification.OneTimePin == Global.OneTimePin)
                {
                    return RedirectToAction("SemesterDetails", "TblStudents");
                }
                else
                {
                    ViewBag.Error = "Invalid pin. Please try again";
                    return View(verification);
                }
            }
            return View(verification);
        }

        // GET: TblStudents/OTP
        public IActionResult SemesterDetails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SemesterDetails([Bind("StartDate,NumberOfWeeks")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                TblStudent tblStudent = new TblStudent();
                tblStudent.StartDate = semester.StartDate;
                tblStudent.NumberOfWeeks = semester.NumberOfWeeks;

                _context.Add(tblStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semester);
        }

        // GET: Users/Signup
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("StudentNumber,StudentName,StudentSurname,StudentEmail,StudentHashPassword")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblStudent tblStudent = new TblStudent();
                    tblStudent.StudentNumber = student.StudentNumber;
                    tblStudent.StudentName = student.StudentName;
                    tblStudent.StudentSurname = student.StudentSurname;
                    tblStudent.StudentEmail = student.StudentEmail;
                    tblStudent.StudentHashPassword = BCrypt.Net.BCrypt.HashPassword(student.StudentHashPassword);

                    Global.StudentNumber = student.StudentNumber;

                    _context.Add(tblStudent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("OTPVerification", "TblStudents");
                }
            }
            catch (DbUpdateException)
            {
                ViewBag.UserExits = "This user already exits. Login using the link below.";
            }

            return View(student);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        PlannerContext db = new PlannerContext();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("StudentNumber,StudentHashPassword")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                // validate if student login details exits
                TblStudent filtStudentDetails = await Task.Run(() => db.TblStudents
                                        .Where(x => x.StudentNumber == tblStudent.StudentNumber)
                                        .FirstOrDefaultAsync());

                // retrive hashed password from database
                string dbHashPassword = await Task.Run(() => db.TblStudents
                                        .Where(x => x.StudentNumber == tblStudent.StudentNumber)
                                        .Select(x => x.StudentHashPassword)
                                        .FirstOrDefaultAsync());

                if (dbHashPassword == null)
                {
                    ViewBag.InvalidDetails = "Invalid credentials. Please try again.";
                    return View(tblStudent);
                }
                else
                {
                    try
                    {
                        // verify if user password is the same as hashed password in the database
                        bool verify = await Task.Run(() => BCrypt.Net.BCrypt.Verify(tblStudent.StudentHashPassword, dbHashPassword));

                        // conditional statement
                        if (filtStudentDetails != null && verify == true)
                        {
                            return RedirectToAction("Index", "Students");
                        }
                        else
                        {
                            ViewBag.InvalidDetails = "Invalid credentials. Please try again.";
                        }
                    }
                    catch (System.ArgumentNullException)
                    {
                        ViewBag.InvalidDetails = "Please enter a valid password.";
                    }
                }
            }
            return View(tblStudent);
        }


        // GET: TblStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblStudent tblStudent = await _context.TblStudents.FindAsync(id);
            if (tblStudent == null)
            {
                return NotFound();
            }
            return View(tblStudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentNumber,StudentName,StudentSurname,StudentEmail,StudentHashPassword,StartDate,NumberOfWeeks")] TblStudent tblStudent)
        {
            if (id != tblStudent.StudentNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblStudentExists(tblStudent.StudentNumber))
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
            return View(tblStudent);
        }

        // GET: TblStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblStudent tblStudent = await _context.TblStudents
                .FirstOrDefaultAsync(m => m.StudentNumber == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // POST: TblStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TblStudent tblStudent = await _context.TblStudents.FindAsync(id);
            _context.TblStudents.Remove(tblStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblStudentExists(int id)
        {
            return _context.TblStudents.Any(e => e.StudentNumber == id);
        }
    }
}
