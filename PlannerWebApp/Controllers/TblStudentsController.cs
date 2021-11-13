using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLibrary.Controllers
{
    public class TblStudentsController : Controller
    {
        private readonly PlannerContext _context;
        PlannerContext db = new PlannerContext();
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

        // GET: TblStudents/OTPVerification
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
                TblStudent tblStudent = _context.TblStudents.FirstOrDefault(x => x.StudentNumber == Global.StudentNumber);
                tblStudent.StartDate = semester.StartDate;
                tblStudent.NumberOfWeeks = semester.NumberOfWeeks;

                Global.StartDate = tblStudent.StartDate;
                Global.StudentNumber = tblStudent.NumberOfWeeks;

                await _context.SaveChangesAsync();
                // maybe include a message to say details have successfully been updated
                return RedirectToAction("Index", "TblModules");
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
        public async Task<IActionResult> Signup([Bind("StudentNumber,StudentName,StudentSurname,StudentEmail,StudentHashPassword")] StudentSignup student)
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
                ViewBag.UserExits = "This user already exits. Please check your details and try again or login below.";
            }
            catch (ArgumentNullException)
            {
                ViewBag.Password = "Please enter a valid password.";
            }

            return View(student);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("StudentNumber,StudentHashPassword")] StudentLogin login)
        {
            if (ModelState.IsValid)
            {
                // validate if student login details exits
                TblStudent filtStudentDetails = await Task.Run(() => db.TblStudents
                                        .Where(x => x.StudentNumber == login.StudentNumber)
                                        .FirstOrDefaultAsync());

                // retrive hashed password from database
                string dbHashPassword = await Task.Run(() => db.TblStudents
                                        .Where(x => x.StudentNumber == login.StudentNumber)
                                        .Select(x => x.StudentHashPassword)
                                        .FirstOrDefaultAsync());

                if (dbHashPassword == null)
                {
                    ViewBag.InvalidDetails = "Invalid credentials. Please try again.";
                    return View(login);
                }
                else
                {
                    try
                    {
                        // verify if user password is the same as hashed password in the database
                        bool verify = await Task.Run(() => BCrypt.Net.BCrypt.Verify(login.StudentHashPassword, dbHashPassword));

                        // conditional statement
                        if (filtStudentDetails != null && verify == true)
                        {
                            Global.StudentNumber = login.StudentNumber;
                            Global.StartDate = await db.TblStudents.Where(x=> x.StudentNumber == Global.StudentNumber).Select(x=> x.StartDate).FirstAsync();
                            Global.NoOfWeeks = await db.TblStudents.Where(x=> x.StudentNumber == Global.StudentNumber).Select(x=> x.NumberOfWeeks).FirstAsync();

                            return RedirectToAction("SemesterDetails", "TblStudents");
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
            return View(login);
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

/*
 * Code Attribution
 *  Topic : How to update record using Entity Framework Core?
 *  Author : H. Herzl
 *  Link awavilable at : https://stackoverflow.com/questions/46657813/how-to-update-record-using-entity-framework-coreanswered [Oct 10 '17 at 4:48]
 *  Code implemented : SemesterDetails [HttpPost] action
 */