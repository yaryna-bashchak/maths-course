using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Course;
using API.Dtos.Section;
using API.Dtos.Lesson;
using API.Repositories;
using API.Repositories.Implementation;
using System.Globalization;

namespace API.Tests;

public class CoursesControllerTests
{
    private Mock<ICoursesRepository> _mockRepository = new Mock<ICoursesRepository>();

    [Fact]
    public async void GetCourse_ValidId()
    {
        //Arrange
        var id = 1;

        var validCourse = new GetCourseDto
        {
            Id = id,
            Title = "Повний курс",
            Duration = 8,
            Sections = new List<GetSectionDto>
            {
                new GetSectionDto
                {
                    Id = 1,
                    Number = 1,
                    Lessons = new List<GetLessonDto>
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
                    },
                },
                new GetSectionDto
                {
                    Id = 2,
                    Number = 2,
                    Lessons = new List<GetLessonDto>
                    {
                        new GetLessonDto
                        {
                            Id = 4,
                            Title = "Вступ в геометрію: фігури на площині, кути",
                        },
                        new GetLessonDto
                        {
                            Id = 9,
                            Title = "Трикутник. 3 ознаки рівності трик., 3 ознаки подібності трик., формула Герона",
                        },
                        new GetLessonDto
                        {
                            Id = 10,
                            Title = "Прямокутний трикутник, теорема Піфагора, sin, cos, tg, ctg, похила",
                        },
                    },
                },
            },
        };

        _mockRepository
            .Setup(x => x.GetCourse(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<GetCourseDto> { IsSuccess = true, Data = validCourse }));

        //Act
        var result = await _mockRepository.Object.GetCourse(id);

        //Assert
        Assert.Equal(validCourse.Id, result.Data.Id);
        Assert.Equal(validCourse.Title, result.Data.Title);
        Assert.Equal(validCourse.Duration, result.Data.Duration);
        Assert.Equal(validCourse.Sections, result.Data.Sections);
    }

    [Fact]
    public async void GetCourse_InvalidId()
    {
        //Arrange
        var id = 15;

        _mockRepository
            .Setup(x => x.GetCourse(It.IsAny<int>()))
            .Returns(Task.FromResult(new Result<GetCourseDto> { IsSuccess = false, ErrorMessage = "Course with the provided ID not found." }));

        //Act
        var result = await _mockRepository.Object.GetCourse(id);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Course with the provided ID not found.", result.ErrorMessage);
    }
}
