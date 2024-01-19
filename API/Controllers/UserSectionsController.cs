using API.Dtos.Section;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserSectionsController : BaseApiController
    {
        private ISectionsRepository _sectionsRepository;
        public UserSectionsController(ISectionsRepository sectionsRepository)
        {
            _sectionsRepository = sectionsRepository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSectionAvailability(int id, UpdateUserSectionDto updatedUserSection)
        {
            var username = User.Identity.Name ?? "";
            var result = await _sectionsRepository.UpdateSectionAvailability(id, updatedUserSection, username);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok();
        }
    }
}