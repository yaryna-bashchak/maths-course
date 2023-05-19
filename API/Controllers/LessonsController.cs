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

        [HttpPost]
        public ActionResult<List<GetLessonDto>> AddLesson(AddLessonDto newLesson)
        {
            var lesson = Mapper.Map<Lesson>(newLesson);
            lesson.Id = Context.Lessons.Max(l => l.Id) + 1;
            Context.Lessons.Add(lesson);
            Context.SaveChanges();
            
            var lessons = Context.Lessons.Select(l => Mapper.Map<GetLessonDto>(l)).ToList();
            return Ok(lessons);
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
            if (dbLesson == null)
            {
                return NotFound("Lesson with the provided ID not found.");
            }
            
            var lesson = Mapper.Map<GetLessonDto>(dbLesson);
            return Ok(lesson);
        }

        [HttpPut("id")]
        public ActionResult<GetLessonDto> UpdateLesson(UpdateLesssonDto updatedLesson)
        {
            var dbLesson = Context.Lessons.FirstOrDefault(l => l.Id == updatedLesson.Id);
            if (dbLesson == null)
            {
                return NotFound("Lesson with the provided ID not found.");
            }

            dbLesson.Title = updatedLesson.Title ?? dbLesson.Title;
            dbLesson.Description = updatedLesson.Description ?? dbLesson.Description;
            dbLesson.UrlTheory = updatedLesson.UrlTheory ?? dbLesson.UrlTheory;
            dbLesson.UrlPractice = updatedLesson.UrlPractice ?? dbLesson.UrlPractice;
            dbLesson.Number = updatedLesson.Number != -1 ? updatedLesson.Number : dbLesson.Number;
            dbLesson.Importance = updatedLesson.Importance != -1 ? updatedLesson.Importance : dbLesson.Importance;
            dbLesson.isCompleted = updatedLesson.isCompleted != -1 ? (updatedLesson.isCompleted != 0) : dbLesson.isCompleted;
            Context.SaveChanges();
            
            var lesson = Mapper.Map<GetLessonDto>(dbLesson);
            return Ok(lesson);
        }
    }
}