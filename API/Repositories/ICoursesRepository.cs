using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Course;

namespace API.Repositories
{
    public interface ICoursesRepository
    {
        Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id);
        Task<Result<GetCourseDto>> GetCourse(int id);
    }
}