using API.Dtos.Lesson;

namespace API.Repositories
{
    public interface ILessonsRepository
    {
        Task<Result<List<GetLessonDto>>> GetLessons();
        Task<Result<GetLessonDto>> GetLesson(int id);
        Task<Result<List<GetLessonDto>>> AddLesson(AddLessonDto newLesson);
        Task<Result<GetLessonDto>> UpdateLesson(UpdateLesssonDto updatedLesson);
        Task<Result<List<GetLessonDto>>> DeleteLesson(int id);
        Task<Result<List<GetLessonDto>>> GetLessonsByKeyword(string keyword);
        Task<Result<List<GetLessonDto>>> GetLessonsByKeywordID(int id);
    }
}