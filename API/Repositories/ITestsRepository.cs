using API.Dtos.Test;

namespace API.Repositories
{
    public interface ITestsRepository
    {
        Task<Result<List<GetTestDto>>> GetTestsOfLesson(int lessonId, string username);
        Task<Result<GetTestDto>> AddTest(AddTestDto newTest, string username);
        Task<Result<GetTestDto>> UpdateTest(int id, UpdateTestDto updatedTest, string username);
        Task<Result<bool>> DeleteTest(int id, string username);
    }
}