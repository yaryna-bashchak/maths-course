using API.Dtos.Option;

namespace API.Repositories
{
    public interface IOptionsRepository
    {
        Task<Result<GetOptionDto>> AddOption(AddOptionDto newOption);
        Task<Result<GetOptionDto>> UpdateOption(int id, UpdateOptionDto updatedOption);
        Task<Result<bool>> DeleteOption(int id);
        Task<Result<bool>> DeleteAllOptionsOfTest(int testId);
    }
}