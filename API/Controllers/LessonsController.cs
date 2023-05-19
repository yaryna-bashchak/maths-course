using API.Data;
using API.Dtos.Lesson;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        public CourseContext Context { get; }
        public IMapper Mapper { get; }
        public LessonsController(CourseContext context, IMapper mapper)
        {
            Mapper = mapper;
            Context = context;
        }

        [HttpGet]
        public ActionResult<List<GetLessonDto>> GetLessons()
        {
            var dbLessons = Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword);
            
            var lessons = dbLessons.Select(l => Mapper.Map<GetLessonDto>(l)).ToList();
            
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public ActionResult<GetLessonDto> GetLesson(int id)
        {
            var dbLesson = Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .FirstOrDefault(l => l.Id == id);

            var lesson = Mapper.Map<GetLessonDto>(dbLesson);
            return Ok(lesson);
        }
        }
    }
}