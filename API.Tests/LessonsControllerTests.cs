using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Reflection;
using API.Dtos.Lesson;
using API.Dtos.Keyword;
using API.Repositories;
using API.Repositories.Implementation;

namespace API.Tests;

public class LessonsControllerTests
{
    private Mock<ILessonsRepository> _mockRepository = new Mock<ILessonsRepository>();
    
    [Fact]
    public async void GetLesson_ValidId()
    {
        //Arrange
        var id = 2;

        var validLesson = new GetLessonDto
        {
            Id = 2,
            Title = "Десяткові дроби, дії з ними, модуль",
            Description = "На уроці ви дізнаєтеся як виконувати додавання, віднімання, множення та ділення десяткових дробів і навчитеся знаходити модуль числа.",
            Keywords = new List<GetKeywordDto>
            {
                new GetKeywordDto
                {
                    Id = 1,
                    Word = "дроби",
                },
                new GetKeywordDto
                {
                    Id = 3,
                    Word = "модуль",
                },
            },
        };

        _mockRepository
            .Setup(x => x.GetLesson(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<GetLessonDto> { IsSuccess = true, Data = validLesson }));

        //Act
        var result = await _mockRepository.Object.GetLesson(id);

        //Assert
        Assert.Equal(validLesson.Id, result.Data.Id);
        Assert.Equal(validLesson.Title, result.Data.Title);
        Assert.Equal(validLesson.Description, result.Data.Description);
        Assert.Equal(validLesson.Keywords, result.Data.Keywords);
    }

    [Fact]
        public async void GetLesson_InvalidId()
    {
        //Arrange
        var id = 25;

        _mockRepository
            .Setup(x => x.GetLesson(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." }));

        //Act
        var result = await _mockRepository.Object.GetLesson(id);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Lesson with the provided ID not found.", result.ErrorMessage);
    }

    [Fact]
    public async void AddLesson_ValidLesson()
    {
        //Arrange
        var id = 21;
        var title = "New lesson from test";
        var description = "Description of new lesson";
        var isCompleted = true;

        var newLesson = new AddLessonDto
        {
            Title = title,
            Description = description,
            IsCompleted = isCompleted,
        };

        var validNewLesson = new GetLessonDto
        {
            Id = id,
            Title = title,
            Description = description,
            IsCompleted = isCompleted,
        };

        _mockRepository
            .Setup(x => x.AddLesson(It.IsAny<AddLessonDto>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = new List<GetLessonDto>{ validNewLesson }}));

        //Act
        var result = await _mockRepository.Object.AddLesson(newLesson);

        //Assert
        Assert.Equal(id, result.Data.Last().Id);
        Assert.Equal(title, result.Data.Last().Title);
        Assert.Equal(description, result.Data.Last().Description);
        Assert.True(result.Data.Last().IsCompleted);
    }
}