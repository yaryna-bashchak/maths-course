using API.Dtos.Test;

namespace API.Repositories
{
    public interface ITestsRepository
    {
        Task<Result<List<GetTestDto>>> GetTestsOfLesson(int lessonId, string username);
        Task<Result<TestsStatisticDto>> GetStatisticsOfLessonTests(TestsStatisticParametersDto parameters, string username);
        Task<Result<GetTestDto>> AddTest(AddTestDto newTest);
        Task<Result<GetTestDto>> UpdateTest(int id, UpdateTestDto updatedTest);
        Task<Result<bool>> DeleteTest(int id);
    }
}