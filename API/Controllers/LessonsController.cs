using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<Lesson>> GetLessons()
        {
            var lessons = Context.Lessons.ToList();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public ActionResult<Lesson> GetLesson(int id)
        {
            return Context.Lessons.Find(id);
        }
    }
}