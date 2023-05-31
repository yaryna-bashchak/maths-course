using API.Dtos.Lesson;

namespace API.Repositories
{
    public interface ILessonsRepository
    {
        List<GetLessonDto> AddLesson(AddLessonDto newLesson);
    }
}