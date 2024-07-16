using API.Dtos.Course;
using API.Dtos.Section;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseAvailabilityController : BaseApiController
    {
        private ISectionsRepository _sectionsRepository;
        public CourseAvailabilityController(ISectionsRepository sectionsRepository)
        {
            _sectionsRepository = sectionsRepository;
        }

        [HttpPut("section/{id}")]
        public async Task<IActionResult> UpdateSectionAvailability(int id, UpdateUserSectionDto updatedUserSection)
        {
            var username = User.Identity.Name ?? "";
            var result = await _sectionsRepository.UpdateSectionAvailability(id, updatedUserSection, username);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPut("course/{id}")]
        public async Task<IActionResult> UpdateCourseAvailability(int id, UpdateUserCourseDto updatedUserCourse)
        {
            var username = User.Identity.Name ?? "";
            var result = await _sectionsRepository.UpdateSectionsAvailabilityByCourseId(id, updatedUserCourse, username);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok();
        }
    }
}