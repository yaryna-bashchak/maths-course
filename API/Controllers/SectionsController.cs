using API.Dtos.Section;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SectionsController : BaseApiController
    {
        private ISectionsRepository _sectionsRepository;
        public SectionsController(ISectionsRepository sectionsRepository)
        {
            _sectionsRepository = sectionsRepository;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetSectionDto>> UpdateSection(int id, UpdateSectionDto updatedSection)
        {
            var result = await _sectionsRepository.UpdateSection(id, updatedSection);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}