using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Reflection;
using API.Dtos.Lesson;
using API.Dtos.Keyword;
using API.Repositories;
using API.Repositories.Implementation;
using System.Linq;

namespace API.Tests;

public class LessonsControllerTests
{
    private Mock<ILessonsRepository> _mockRepository = new Mock<ILessonsRepository>();
    
    [Fact]
    public async void GetLessons()
    {
        //Arrange
        var validLessons = new List<GetLessonDto>
        {
            new GetLessonDto
            {
                Id = 1,
                Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
            },
            new GetLessonDto
            {
                Id = 2,
                Title = "Десяткові дроби, дії з ними, модуль",
            },
            new GetLessonDto
            {
                Id = 3,
                Title = "Вступ в геометрію: фігури на площині, кути",
            },
        };

        _mockRepository
            .Setup(x => x.GetLessons())
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = validLessons }));

        //Act
        var result = await _mockRepository.Object.GetLessons();

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validLessons.Count, result.Data.Count);
        Assert.Equal(validLessons.First().Id, result.Data.First().Id);
        Assert.Equal(validLessons.Last().Id, result.Data.Last().Id);
    }

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

    [Fact]
    public async void UpdateLesson_ValidId()
    {
        //Arrange
        var id = 2;
        var title = "(назву змінено)";
        var importance = 1;


        var updatedLesson = new UpdateLesssonDto
        {
            Id = id,
            Title = title,
            Importance = importance,
        };

        var validUpdatedLesson = new GetLessonDto
        {
            Id = id,
            Title = title,
            Description = "На уроці ви дізнаєтеся як виконувати додавання, віднімання, множення та ділення десяткових дробів і навчитеся знаходити модуль числа.",
            Number = 2,
            Importance = importance,
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
            .Setup(x => x.UpdateLesson(It.IsAny<UpdateLesssonDto>()))
            .Returns(Task.FromResult(new Result<GetLessonDto> { IsSuccess = true, Data = validUpdatedLesson }));

        //Act
        var result = await _mockRepository.Object.UpdateLesson(updatedLesson);

        //Assert
        Assert.Equal(id, result.Data.Id);
        Assert.Equal(title, result.Data.Title);
        Assert.Equal(importance, result.Data.Importance);
        Assert.Equal(validUpdatedLesson.Description, result.Data.Description);
        Assert.Equal(validUpdatedLesson.Number, result.Data.Number);
        Assert.Equal(validUpdatedLesson.Keywords, result.Data.Keywords);
    }

    [Fact]
    public async void UpdateLesson_InvalidId()
    {
        //Arrange
        var id = -3;
        var importance = 2;

        var updatedLesson = new UpdateLesssonDto
        {
            Id = id,
            Importance = importance,
        };

        _mockRepository
            .Setup(x => x.UpdateLesson(It.IsAny<UpdateLesssonDto>()))
            .Returns(Task.FromResult(new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." }));

        //Act
        var result = await _mockRepository.Object.UpdateLesson(updatedLesson);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Lesson with the provided ID not found.", result.ErrorMessage);
    }

    [Fact]
    public async void DeleteLesson_ValidId()
    {
        //Arrange
        var id = 3;

        var validLessons = new List<GetLessonDto>
        {
            new GetLessonDto
            {
                Id = 1,
                Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
            },
            new GetLessonDto
            {
                Id = 2,
                Title = "Десяткові дроби, дії з ними, модуль",
            },
            new GetLessonDto
            {
                Id = 4,
                Title = "Вступ в геометрію: фігури на площині, кути",
            },
        };

        _mockRepository
            .Setup(x => x.DeleteLesson(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = validLessons }));

        //Act
        var result = await _mockRepository.Object.DeleteLesson(id);

        //Assert
        Assert.Equal(3, result.Data.Count);
        
        var deletedLesson = result.Data.FirstOrDefault(l => l.Id == id);
        Assert.Null(deletedLesson);
    }

    [Fact]
    public async void DeleteLesson_InvalidId()
    {
        //Arrange
        var id = 100;

        _mockRepository
            .Setup(x => x.DeleteLesson(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." }));

        //Act
        var result = await _mockRepository.Object.DeleteLesson(id);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Lesson with the provided ID not found.", result.ErrorMessage);
    }

    [Fact]
    public async void GetLessonsByKeyword_TwoKeywordsMatch()
    {
        //Arrange
        var keywordPattern = "вираз";

        var validLessons = new List<GetLessonDto>
        {
            new GetLessonDto
            {
                Id = 20,
                Title = "Показниковий вираз",
                Keywords = new List<GetKeywordDto>
                {
                    new GetKeywordDto
                    {
                        Word = "рівняння",
                    },
                    new GetKeywordDto
                    {
                        Word = "показниковий вираз",
                    },
                    new GetKeywordDto
                    {
                        Word = "показникові рівняння",
                    },
                },
            },
            new GetLessonDto
            {
                Id = 21,
                Title = "Логарифмічний вираз",
                Keywords = new List<GetKeywordDto>
                {
                    new GetKeywordDto
                    {
                        Word = "рівняння",
                    },
                    new GetKeywordDto
                    {
                        Word = "логарифмічний вираз",
                    },
                    new GetKeywordDto
                    {
                        Word = "логарифмічні рівняння",
                    },
                },
            },
        };

        _mockRepository
            .Setup(x => x.GetLessonsByKeyword(It.IsAny<string>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = validLessons }));

        //Act
        var result = await _mockRepository.Object.GetLessonsByKeyword(keywordPattern);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validLessons.Count, result.Data.Count);
        Assert.Equal(validLessons.First().Id, result.Data.First().Id);
        Assert.Equal(validLessons.Last().Id, result.Data.Last().Id);
        Assert.Contains("показниковий вираз", result.Data[0].Keywords.Select(k => k.Word));
        Assert.Contains("логарифмічний вираз", result.Data[1].Keywords.Select(k => k.Word));
    }

    [Fact]
    public async void GetLessonsByKeyword_NoKeywordMatch()
    {
        //Arrange
        var keywordPattern = "матем";

        _mockRepository
            .Setup(x => x.GetLessonsByKeyword(It.IsAny<string>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Lessons with this keyword pattern not found." }));

        //Act
        var result = await _mockRepository.Object.GetLessonsByKeyword(keywordPattern);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Lessons with this keyword pattern not found.", result.ErrorMessage);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    public async void GetLessonsByImportance_EqualOrLessThan2(int importance)
    {
        //Arrange
        var validLessons = new List<GetLessonDto>
        {
            new GetLessonDto
            {
                Id = 1,
                Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 2,
                Title = "Десяткові дроби, дії з ними, модуль",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 3,
                Title = "Вступ в геометрію: фігури на площині, кути",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 17,
                Title = "Складніші рівняння, теорема Безу",
                Importance = 2,
            },
            new GetLessonDto
            {
                Id = 18,
                Title = "Складніші рівняння, част 2",
                Importance = 1,
            },
        };

        _mockRepository
            .Setup(x => x.GetLessonsByImportance(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = validLessons }));

        //Act
        var result = await _mockRepository.Object.GetLessonsByImportance(importance);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validLessons.Count, result.Data.Count);
        Assert.Contains(0, result.Data.Select(l => l.Importance));
        Assert.Contains(1, result.Data.Select(l => l.Importance));
        Assert.Contains(2, result.Data.Select(l => l.Importance));
        Assert.Equal(2, result.Data[3].Importance);
        Assert.Equal(1, result.Data[4].Importance);
    }

    [Fact]
    public async void GetLessonsByImportance_EqualOrLessThan1()
    {
        //Arrange
        int importance = 1;

        var validLessons = new List<GetLessonDto>
        {
            new GetLessonDto
            {
                Id = 1,
                Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 2,
                Title = "Десяткові дроби, дії з ними, модуль",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 3,
                Title = "Вступ в геометрію: фігури на площині, кути",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 18,
                Title = "Складніші рівняння, част 2",
                Importance = 1,
            },
        };

        _mockRepository
            .Setup(x => x.GetLessonsByImportance(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = validLessons }));

        //Act
        var result = await _mockRepository.Object.GetLessonsByImportance(importance);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validLessons.Count, result.Data.Count);
        Assert.Contains(0, result.Data.Select(l => l.Importance));
        Assert.Contains(1, result.Data.Select(l => l.Importance));
        Assert.DoesNotContain(2, result.Data.Select(l => l.Importance));
        Assert.Equal(1, result.Data[3].Importance);
    }

    [Fact]
    public async void GetLessonsByImportance_EqualOrLessThan0()
    {
        //Arrange
        int importance = 0;

        var validLessons = new List<GetLessonDto>
        {
            new GetLessonDto
            {
                Id = 1,
                Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 2,
                Title = "Десяткові дроби, дії з ними, модуль",
                Importance = 0,
            },
            new GetLessonDto
            {
                Id = 3,
                Title = "Вступ в геометрію: фігури на площині, кути",
                Importance = 0,
            },
        };

        _mockRepository
            .Setup(x => x.GetLessonsByImportance(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = true, Data = validLessons }));

        //Act
        var result = await _mockRepository.Object.GetLessonsByImportance(importance);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validLessons.Count, result.Data.Count);
        Assert.Contains(0, result.Data.Select(l => l.Importance));
        Assert.DoesNotContain(1, result.Data.Select(l => l.Importance));
        Assert.DoesNotContain(2, result.Data.Select(l => l.Importance));
    }

    [Fact]
    public async void GetLessonsByImportance_Invalid()
    {
        //Arrange
        int importance = -3;

        _mockRepository
            .Setup(x => x.GetLessonsByImportance(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Importance cannot be less than 0 (it can only be 0, 1 or 2)."}));

        //Act
        var result = await _mockRepository.Object.GetLessonsByImportance(importance);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Importance cannot be less than 0 (it can only be 0, 1 or 2).", result.ErrorMessage);
    }

    [Fact]
    public async void GetLessonsByImportance_EmptyDatabase()
    {
        //Arrange
        int importance = 1;

        _mockRepository
            .Setup(x => x.GetLessonsByImportance(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "It seems there are no lessons whose importance is equal to or less than given one (it can only be 0, 1 or 2)."}));

        //Act
        var result = await _mockRepository.Object.GetLessonsByImportance(importance);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("It seems there are no lessons whose importance is equal to or less than given one (it can only be 0, 1 or 2).", result.ErrorMessage);
    }
}