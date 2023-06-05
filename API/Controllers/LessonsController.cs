using API.Data;
using API.Dtos.Lesson;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private CourseContext _context;
        private IMapper _mapper;
        private ILessonsRepository _lessonsRepository;
        public LessonsController(
            CourseContext context,
            IMapper mapper,
            ILessonsRepository lessonsRepository)
        {
            _mapper = mapper;
            _context = context;
            _lessonsRepository = lessonsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<List<GetLessonDto>>> AddLesson(AddLessonDto newLesson)
        {
            var result = await _lessonsRepository.AddLesson(newLesson);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetLessonDto>>> GetLessons()
        {
            var result = await _lessonsRepository.GetLessons();

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetLessonDto>> GetLesson(int id)
        {
            var result = await _lessonsRepository.GetLesson(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpGet("keyword/{keyword}")]
        public async Task<ActionResult<List<GetLessonDto>>> GetLessonsByKeyword(string keyword)
        {
            var result = await _lessonsRepository.GetLessonsByKeyword(keyword);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpGet("keyword/id/{id}")]
        public async Task<ActionResult<List<GetLessonDto>>> GetLessonsByKeywordId(int id)
        {
            var result = await _lessonsRepository.GetLessonsByKeywordID(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpGet("importance/{importance}")]
        public async Task<ActionResult<List<GetLessonDto>>> GetLessonsByImportance(int importance)
        {
            var result = await _lessonsRepository.GetLessonsByImportance(importance);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpPut("id")]
        public async Task<ActionResult<GetLessonDto>> UpdateLesson(UpdateLesssonDto updatedLesson)
        {
            var result = await _lessonsRepository.UpdateLesson(updatedLesson);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetLessonDto>>> DeleteLesson(int id)
        {
            var result = await _lessonsRepository.DeleteLesson(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}