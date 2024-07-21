using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Course;
using API.Dtos.Section;

namespace API.Repositories
{
    public interface ISectionsRepository
    {
        // Task<Result<GetSectionDto>> GetSection(int id);
        Task<Result<GetSectionDto>> AddSection(AddSectionDto newSection, string username);
        Task<Result<GetSectionDto>> UpdateSection(int id, UpdateSectionDto updatedSection, string username);
        Task<Result<GetSectionDto>> MakeSectionAvailable(int sectionId, string userId);
        Task<Result<GetCourseDto>> MakeCourseAvailable(int courseId, string userId);
        Task<Result<bool>> DeleteSection(int id);
    }
}