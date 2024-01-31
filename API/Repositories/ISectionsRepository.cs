using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Section;

namespace API.Repositories
{
    public interface ISectionsRepository
    {
        // Task<Result<GetSectionDto>> GetSection(int id);
        Task<Result<GetSectionDto>> AddSection(AddSectionDto newSection, string username);
        Task<Result<GetSectionDto>> UpdateSection(int id, UpdateSectionDto updatedSection, string username);
        Task<Result<GetSectionDto>> UpdateSectionAvailability(int sectionId, UpdateUserSectionDto updatedUserSection, string username);
        Task<Result<bool>> DeleteSection(int id);
    }
}