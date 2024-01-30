using API.Dtos.Course;
using API.Repositories;
using API.RequestHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CoursesController : BaseApiController
    {
        private ICoursesRepository _coursesRepository;
        public CoursesController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCoursePreviewDto>>> GetCourses()
        {
            var username = User.Identity.Name ?? "";
            var result = await _coursesRepository.GetCourses(username);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
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
        public async Task<ActionResult<GetCourseDto>> GetCourse(int id, [FromQuery] LessonParams lessonParams)
        {
            var username = User.Identity.Name ?? "";
            var result = await _coursesRepository.GetCourse(id, lessonParams.MaxImportance, lessonParams.OnlyUncompleted, lessonParams.SearchTerm, username);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}