using API.Dtos.Test;

namespace API.Repositories
{
    public interface ITestsRepository
    {
        Task<Result<List<GetTestDto>>> GetTestsOfLesson(int lessonId, string username);
        Task<Result<List<GetTestDto>>> AddTest(AddTestDto newTest, string username);
        Task<Result<List<GetTestDto>>> DeleteTest(int id, string username);
    }
}