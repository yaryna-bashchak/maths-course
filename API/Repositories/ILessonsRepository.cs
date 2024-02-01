using API.Dtos.Course;
using API.Dtos.Lesson;

namespace API.Repositories
{
    public interface ILessonsRepository
    {
        Task<Result<List<GetLessonDto>>> GetLessons();
        Task<Result<GetLessonDto>> GetLesson(int id, string username);
        Task<Result<GetLessonDto>> AddLesson(AddLessonDto newLesson, string username);
        Task<Result<GetLessonDto>> UpdateLesson(int id, UpdateLesssonDto updatedLesson, string username);
        Task<Result<GetLessonDto>> UpdateLessonCompletion(int lessonId, UpdateUserLesssonDto updatedUserLesson, GetCourseDto course, string username);
        Task<Result<bool>> DeleteLesson(int id);
        // Task<Result<List<GetLessonDto>>> GetLessonsByKeyword(string keyword);
        // Task<Result<List<GetLessonDto>>> GetLessonsByKeywordID(int id);
        // Task<Result<List<GetLessonDto>>> GetLessonsByImportance(int importance);
    }
}