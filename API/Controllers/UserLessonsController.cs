using API.Dtos.Lesson;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserLessonsController : BaseApiController
{
    private ILessonsRepository _lessonsRepository;
    private readonly ICoursesRepository _coursesRepository;
    public UserLessonsController(ILessonsRepository lessonsRepository, ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
        _lessonsRepository = lessonsRepository;
    }

    [HttpPut("{lessonId}")]
    public async Task<IActionResult> UpdateLessonCompletion(int lessonId, UpdateUserLesssonDto updatedUserLesson)
    {
        var username = User.Identity.Name ?? "";

        var courseResult = await _coursesRepository.GetCourse(updatedUserLesson.courseId, null, null, "", username);
        if (!courseResult.IsSuccess)
            return Unauthorized(courseResult.ErrorMessage);

        var result = await _lessonsRepository.UpdateLessonCompletion(lessonId, updatedUserLesson, courseResult.Data, username);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessage.Contains("Unauthorized")) return Unauthorized();
            return NotFound(result.ErrorMessage);
        }

        return Ok(result.Data);
    }
}
