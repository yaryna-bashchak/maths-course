using API.Dtos.Section;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionsController : ControllerBase
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