using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        public CourseContext Context { get; }
        public LessonsController(CourseContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> GetLessons()
        {
            var lessons = await Context.Lessons.ToListAsync();
            return lessons;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            return await Context.Lessons.FindAsync(id);
        }
    }
}