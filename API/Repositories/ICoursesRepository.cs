using API.Dtos.Course;

namespace API.Repositories
{
    public interface ICoursesRepository
    {
        Task<Result<GetCourseDto>> GetCourse(int id, int? maxImportance, bool? onlyUncompleted, string searchTerm);
        Task<Result<List<GetCoursePreviewDto>>> GetCourses();
        Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id);
    }
}