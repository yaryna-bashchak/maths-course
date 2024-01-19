using API.Data;
using API.Dtos.Test;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation;

public class TestsRepository : ITestsRepository
{
    private CourseContext _context;
    private IMapper _mapper;
    private readonly ImageService _imageService;
    private readonly UserManager<User> _userManager;
    public TestsRepository(
        CourseContext context,
        UserManager<User> userManager,
        IMapper mapper,
        ImageService imageService)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _imageService = imageService;
    }

    public async Task<Result<List<GetTestDto>>> GetTestsOfLesson(int lessonId, string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = "Unauthorized user." };

        var dbLesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == lessonId);

        if (dbLesson == null) return new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };

        var tests = await _context.Tests
            .Where(t => t.Lesson.Id == lessonId)
            .Include(t => t.Options)
            .Select(t => _mapper.Map<GetTestDto>(t))
            .ToListAsync();

        return new Result<List<GetTestDto>> { IsSuccess = true, Data = tests };
    }

    public async Task<Result<GetTestDto>> AddTest(AddTestDto newTest, string username)
    {
        try
        {
            var test = _mapper.Map<Test>(newTest);

            if (newTest.File != null)
            {
                var result = await _imageService.ProcessImageAsync(newTest.File, null, (url, id) => { test.ImgUrl = url; test.PublicId = id; });
                if (!result.IsSuccess) return new Result<GetTestDto> { IsSuccess = false, ErrorMessage = result.ErrorMessage }; ;
            }

            test.Id = _context.Tests.Max(t => t.Id) + 1;

            await _context.Tests.AddAsync(test);

            return await SaveChangesAndReturnResult(test.Id);
        }
        catch (System.Exception ex)
        {
            return new Result<GetTestDto> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<Result<GetTestDto>> UpdateTest(int id, UpdateTestDto updatedTest, string username)
    {
        var dbTest = await _context.Tests.FirstOrDefaultAsync(t => t.Id == id);

        if (dbTest == null) return new Result<GetTestDto> { IsSuccess = false, ErrorMessage = "Test with the provided ID not found." };

        UpdateTestDetails(dbTest, updatedTest);

        if (updatedTest.File != null)
        {
            var result = await _imageService.ProcessImageAsync(updatedTest.File, dbTest.PublicId, (url, id) => { dbTest.ImgUrl = url; dbTest.PublicId = id; });
            if (!result.IsSuccess) return new Result<GetTestDto> { IsSuccess = false, ErrorMessage = result.ErrorMessage };
        }

        return await SaveChangesAndReturnResult(id);
    }

    public async Task<Result<bool>> DeleteTest(int id, string username)
    {
        try
        {
            var dbTest = await _context.Tests.FirstOrDefaultAsync(t => t.Id == id);

            if (dbTest == null) return new Result<bool> { IsSuccess = false, ErrorMessage = "Test with the provided ID not found." };

            if (!string.IsNullOrEmpty(dbTest.PublicId))
                await _imageService.DeleteImageAsync(dbTest.PublicId);

            _context.Tests.Remove(dbTest);
            await _context.SaveChangesAsync();

            return new Result<bool> { IsSuccess = true, Data = true };
        }
        catch (System.Exception ex)
        {
            return new Result<bool> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    private void UpdateTestDetails(Test dbTest, UpdateTestDto updatedTest)
    {
        dbTest.Question = updatedTest.Question ?? dbTest.Question;
        dbTest.Type = updatedTest.Type ?? dbTest.Type;
    }

    private async Task<Result<GetTestDto>> SaveChangesAndReturnResult(int testId, string errorMessage = "Error occurs during saving changes.")
    {
        if (await _context.SaveChangesAsync() > 0)
        {
            var dbTest = await _context.Tests
                .Include(t => t.Options)
                .FirstOrDefaultAsync(t => t.Id == testId);

            return new Result<GetTestDto> { IsSuccess = true, Data = _mapper.Map<GetTestDto>(dbTest) };
        }

        return new Result<GetTestDto> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
