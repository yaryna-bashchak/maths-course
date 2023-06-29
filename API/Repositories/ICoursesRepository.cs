using API.Dtos.Course;

namespace API.Repositories
{
    public interface ICoursesRepository
    {
        Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id);
        Task<Result<GetCourseDto>> GetCourse(int id);
    }
}