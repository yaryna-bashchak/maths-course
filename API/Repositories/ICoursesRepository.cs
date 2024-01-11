using API.Dtos.Course;

namespace API.Repositories
{
    public interface ICoursesRepository
    {
        Task<Result<GetCourseDto>> GetCourse(int id, int? maxImportance, bool? onlyUncompleted, string searchTerm, string username);
        Task<Result<List<GetCoursePreviewDto>>> GetCourses(string username);
        Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id);
    }
}