using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Lesson;
using API.Dtos.Course;
using API.Repositories;
using API.Repositories.Implementation;

namespace API.Tests;

public class CoursesControllerTests
{
    private Mock<ICoursesRepository> _mockRepository = new Mock<ICoursesRepository>();
    
    [Fact]
    public async void GetCourse_ValidId()
    {
        //Arrange
        var id = 4;

        var validCourse = new GetCourseDto
        {
            Id = 4,
            Title = "Геометрія",
            Description = "Цей курс містить всі теми з геометрії, що потрібні для ЗНО/НМТ.",
            Lessons = new List<GetLessonForCourseDto>
            {
                new GetLessonForCourseDto
                {
                    Id = 4,
                    Title = "Вступ в геометрію: фігури на площині, кути",
                },
                new GetLessonForCourseDto
                {
                    Id = 9,
                    Title = "Трикутник. 3 ознаки рівності трик., 3 ознаки подібності трик., формула Герона",
                },
                new GetLessonForCourseDto
                {
                    Id = 10,
                    Title = "Прямокутний трикутник, теорема Піфагора, sin, cos, tg, ctg, похила",
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
        Assert.Equal(validCourse.Description, result.Data.Description);
        Assert.Equal(validCourse.Lessons, result.Data.Lessons);
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
