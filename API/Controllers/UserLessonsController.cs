using API.Dtos.Lesson;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserLessonsController : BaseApiController
{
    private ILessonsRepository _lessonsRepository;
    public UserLessonsController(ILessonsRepository lessonsRepository)
    {
        _lessonsRepository = lessonsRepository;
    }

    [HttpPut("{lessonId}")]
    public async Task<IActionResult> UpdateLessonCompletion(int lessonId, UpdateUserLesssonDto updatedUserLesson)
    {
        var username = User.Identity.Name ?? "";
        var result = await _lessonsRepository.UpdateLessonCompletion(lessonId, updatedUserLesson, username);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessage.Contains("Unauthorized")) return Unauthorized();
            return NotFound(result.ErrorMessage);
        }

        return Ok(result.Data);
    }
}
