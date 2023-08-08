using API.Dtos.Course;

namespace API.Repositories
{
    public interface ICoursesRepository
    {
        Task<Result<List<GetCoursePreviewDto>>> GetCourses();
        Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id);
        Task<Result<GetCourseDto>> GetCourse(int id);
    }
}