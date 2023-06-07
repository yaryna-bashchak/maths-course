using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Test;
using API.Entities;
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

        public async Task<Result<List<GetTestDto>>> AddTest(AddTestDto newTest)
        {
            try
            {
                var test = _mapper.Map<Test>(newTest);
                test.Id = _context.Tests.Max(t => t.Id) + 1;
                await _context.Tests.AddAsync(test);
                await _context.SaveChangesAsync();

                var result = await GetTestsOfLesson(newTest.LessonId);
                return new Result<List<GetTestDto>> { IsSuccess = true, Data = result.Data };
            }
            catch (System.Exception)
            {
                return new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };
            }
        }

        public async Task<Result<List<GetTestDto>>> DeleteTest(int id)
        {
            try
            {
                var dbTest = await _context.Tests.FirstAsync(t => t.Id == id);
                var lessonId = dbTest.LessonId;

                _context.Tests.Remove(dbTest);
                await _context.SaveChangesAsync();

                var result = await GetTestsOfLesson(lessonId);

                if (result.IsSuccess == false)
                {
                    return new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = result.ErrorMessage };
                }

                return new Result<List<GetTestDto>> { IsSuccess = true, Data = result.Data };
            }
            catch (System.Exception)
            {
                return new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = "Test with the provided ID not found." };
            }
        }
    }
}