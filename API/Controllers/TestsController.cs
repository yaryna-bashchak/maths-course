using API.Dtos.Test;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestsController : BaseApiController
    {
        private ITestsRepository _testsRepository;
        public TestsController(ITestsRepository testsRepository)
        {
            _testsRepository = testsRepository;
        }

        [HttpGet("lessonId/{lessonId}")]
        public async Task<ActionResult<List<GetTestDto>>> GetTestsOfLesson(int lessonId)
        {
            var result = await _testsRepository.GetTestsOfLesson(lessonId);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpPost]
        public async Task<ActionResult<List<GetTestDto>>> AddTest(AddTestDto newTest)
        {
            var result = await _testsRepository.AddTest(newTest);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }

        [HttpDelete]
        public async Task<ActionResult<List<GetTestDto>>> DeleteTest(int id)
        {
            var result = await _testsRepository.DeleteTest(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}