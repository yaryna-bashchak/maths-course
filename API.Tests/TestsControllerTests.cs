using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Test;
using API.Repositories;
using API.Repositories.Implementation;

namespace API.Tests;

public class TestsControllerTests
{
    private Mock<ITestsRepository> _mockRepository = new Mock<ITestsRepository>();

    [Fact]
    public async void GetTestsOfLesson_ValidId()
    {
        //Arrange
        var id = 1;

        var validTests = new List<GetTestDto>
        {
            new GetTestDto
            {
                Id = 1,
                Question = "1. Вкажіть натуральне число",
            },
            new GetTestDto
            {
                Id = 2,
                Question = "2. При діленні числа 28 на 6 остача дорівнює",
            },
            new GetTestDto
            {
                Id = 3,
                Question = "3. Скоротіть дріб 108/18 до нескоротного",
            },
        };

        _mockRepository
            .Setup(x => x.GetTestsOfLesson(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetTestDto>> { IsSuccess = true, Data = validTests }));

        //Act
        var result = await _mockRepository.Object.GetTestsOfLesson(id);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validTests.Count, result.Data.Count);
        Assert.Equal(validTests.First().Id, result.Data.First().Id);
        Assert.Equal(validTests.Last().Id, result.Data.Last().Id);
    }

    [Fact]
    public async void GetTestsOfLesson_InvalidId()
    {
        //Arrange
        var id = 30;

        _mockRepository
            .Setup(x => x.GetTestsOfLesson(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetTestDto>> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." }));

        //Act
        var result = await _mockRepository.Object.GetTestsOfLesson(id);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Lesson with the provided ID not found.", result.ErrorMessage);
    }
}
