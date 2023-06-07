using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Test;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class TestsRepository : ITestsRepository
    {
        private CourseContext _context;
        private IMapper _mapper;
        public TestsRepository(
            CourseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<GetTestDto>>> GetTestsOfLesson(int lessonId)
        {
            var dbTests = _context.Tests
                .Include(t => t.Options)
                .Where(t => t.Lesson.Id == lessonId);

            if (dbTests == null || !dbTests.Any())
            {
                return new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = "Tests for this lesson not found." };
            }

            var tests = await dbTests.Select(t => _mapper.Map<GetTestDto>(t)).ToListAsync();
            return new Result<List<GetTestDto>> { IsSuccess = true, Data = tests };
        }
    }
}