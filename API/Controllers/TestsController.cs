using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Test;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
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
    }
}