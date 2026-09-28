using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Student data saved successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var studentsList = await _context.Students
                                       .Include(s => s.CourseResults)
                                       .ToListAsync();

            return Ok(studentsList);
        }
    }
}