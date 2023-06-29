using API.Dtos.Course;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private ICoursesRepository _coursesRepository;
        public CoursesController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [HttpGet("preview/{id}")]
        public async Task<ActionResult<GetCoursePreviewDto>> GetCoursePreview(int id)
        {
            var result = await _coursesRepository.GetCoursePreview(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCourseDto>> GetCourse(int id)
        {
            var result = await _coursesRepository.GetCourse(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}