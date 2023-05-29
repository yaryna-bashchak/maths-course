using System.Text;
using System.Text.RegularExpressions;
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
        public async Task<ActionResult<List<GetLessonDto>>> AddLesson(AddLessonDto newLesson)
        {
            var lesson = Mapper.Map<Lesson>(newLesson);
            lesson.Id = Context.Lessons.Max(l => l.Id) + 1;
            await Context.Lessons.AddAsync(lesson);
            await Context.SaveChangesAsync();
            
            var lessons = await Context.Lessons.Select(l => Mapper.Map<GetLessonDto>(l)).ToListAsync();
            return lessons;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetLessonDto>>> GetLessons()
        {
            var dbLessons = Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson);
            
            var lessons = await dbLessons.Select(l => Mapper.Map<GetLessonDto>(l)).ToListAsync();
            
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetLessonDto>> GetLesson(int id)
        {
            try
            {
                var dbLesson = await Context.Lessons
                    .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                    .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                    .FirstAsync(l => l.Id == id);

                var lesson = Mapper.Map<GetLessonDto>(dbLesson);
                return lesson;
            }
            catch (System.Exception)
            {
                return NotFound("Lesson with the provided ID not found.");
            }
        }

        [HttpGet("keyword/{keyword}")]
        public async Task<ActionResult<GetLessonDto>> GetLessonsByKeyword(string keyword)
        {
            var regex = new Regex(keyword, RegexOptions.IgnoreCase);

            var dbLessons = await Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                .ToListAsync();

            var lessons = dbLessons
                .Select(l => Mapper.Map<GetLessonDto>(l))
                .Where(l => l.Keywords.Any(lk => regex.IsMatch(lk.Word)))
                .ToList();

            if (lessons == null || !lessons.Any())
            {
                return NotFound("Lessons with this keyword pattern not found.");
            }

            return Ok(lessons);
        }

        [HttpGet("keyword/id/{id}")]
        public async Task<ActionResult<GetLessonDto>> GetLessonsByKeywordId(int id)
        {
            var dbLessons = Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                .Where(l => l.LessonKeywords.Any(lk => lk.Keyword.Id == id));

            var lessons = await dbLessons
                .Select(l => Mapper.Map<GetLessonDto>(l)).ToListAsync();
            
            if (lessons == null || !lessons.Any())
            {
                return NotFound("Lessons with this keyword ID not found.");
            }

            return Ok(lessons);
        }

        [HttpPut("id")]
        public async Task<ActionResult<GetLessonDto>> UpdateLesson(UpdateLesssonDto updatedLesson)
        {
            try
            {
                var dbLesson = await Context.Lessons.FirstOrDefaultAsync(l => l.Id == updatedLesson.Id);
                
                dbLesson.Title = updatedLesson.Title ?? dbLesson.Title;
                dbLesson.Description = updatedLesson.Description ?? dbLesson.Description;
                dbLesson.UrlTheory = updatedLesson.UrlTheory ?? dbLesson.UrlTheory;
                dbLesson.UrlPractice = updatedLesson.UrlPractice ?? dbLesson.UrlPractice;
                dbLesson.Number = updatedLesson.Number != -1 ? updatedLesson.Number : dbLesson.Number;
                dbLesson.Importance = updatedLesson.Importance != -1 ? updatedLesson.Importance : dbLesson.Importance;
                dbLesson.isCompleted = updatedLesson.isCompleted != -1 ? (updatedLesson.isCompleted != 0) : dbLesson.isCompleted;
                await Context.SaveChangesAsync();
                
                var lesson = Mapper.Map<GetLessonDto>(dbLesson);
                return lesson;
            }
            catch (System.Exception)
            {
                return NotFound("Lesson with the provided ID not found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetLessonDto>>> DeleteLesson(int id)
        {
            try
            {
                var dbLesson = await Context.Lessons.FirstAsync(l => l.Id == id);
                
                Context.Lessons.Remove(dbLesson);
                await Context.SaveChangesAsync();
            
                var lessons = Context.Lessons.Select(l => Mapper.Map<GetLessonDto>(l)).ToList();
                return lessons;
            }
            catch (System.Exception)
            {
                return NotFound("Lesson with the provided ID not found.");
            }
        }
    }
}