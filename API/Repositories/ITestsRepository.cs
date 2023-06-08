using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Test;

namespace API.Repositories
{
    public interface ITestsRepository
    {
        Task<Result<List<GetTestDto>>> GetTestsOfLesson(int lessonId);
        Task<Result<List<GetTestDto>>> AddTest(AddTestDto newTest);
        Task<Result<List<GetTestDto>>> DeleteTest(int id);
    }
}