using API.Data;
using API.Dtos.Lesson;
using API.Dtos.LessonKeyword;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonKeywordController : ControllerBase
    {
        public CourseContext Context { get; }
        public IMapper Mapper { get; }
        public LessonKeywordController(CourseContext context, IMapper mapper)
        {
            Mapper = mapper;
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<GetLessonDto>> AddLessonKeyword(AddLessonKeywordDto newLessonKeyword)
        {
            Lesson lesson = await Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .FirstOrDefaultAsync(l => l.Id == newLessonKeyword.LessonId);

            if (lesson == null)
            {
                return NotFound("Lesson with the provided ID not found.");
            }

            Keyword keyword = await Context.Keywords
                .FirstOrDefaultAsync(k => k.Id == newLessonKeyword.KeywordId);

            if (keyword == null)
            {
                return NotFound("Keyword with the provided ID not found.");
            }

            LessonKeyword lessonKeyword = new LessonKeyword
            {
                Lesson = lesson,
                Keyword = keyword,
            };

            await Context.LessonKeywords.AddAsync(lessonKeyword);
            await Context.SaveChangesAsync();

            return Mapper.Map<GetLessonDto>(lesson);
        }

        [HttpGet]
        public async Task<ActionResult<List<AddLessonKeywordDto>>> GetLessonKeywords()
        {
            var dbLessonKeywords = Context.LessonKeywords;

            var lessonKeywords = await dbLessonKeywords
                .Select(lk => Mapper.Map<AddLessonKeywordDto>(lk)).ToListAsync();
            
            return lessonKeywords;
        }
    }
}