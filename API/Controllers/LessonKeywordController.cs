using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<GetLessonDto> AddLessonKeyword(AddLessonKeywordDto newLessonKeyword)
        {
            Lesson lesson = Context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .FirstOrDefault(l => l.Id == newLessonKeyword.LessonId);

            if (lesson == null)
            {
                return NotFound("Lesson with the provided ID not found.");
            }

            Keyword keyword = Context.Keywords
                .FirstOrDefault(k => k.Id == newLessonKeyword.KeywordId);

            if (keyword == null)
            {
                return NotFound("Keyword with the provided ID not found.");
            }

            LessonKeyword lessonKeyword = new LessonKeyword
            {
                Lesson = lesson,
                Keyword = keyword,
            };

            Context.LessonKeywords.Add(lessonKeyword);
            Context.SaveChanges();

            return Ok(Mapper.Map<GetLessonDto>(lesson));
        }

        
    }
}