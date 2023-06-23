using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Course;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class CoursesRepository : ICoursesRepository
    {
        private CourseContext _context;
        private IMapper _mapper;
        public CoursesRepository(
            CourseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id)
        {
            try
            {
                var dbCourse = await _context.Courses
                    .Include(c => c.CourseLessons).ThenInclude(cl => cl.Lesson)
                    .FirstAsync(l => l.Id == id);

                var course = _mapper.Map<GetCoursePreviewDto>(dbCourse);
                return new Result<GetCoursePreviewDto> { IsSuccess = true, Data = course };
            }
            catch (System.Exception)
            {
                return new Result<GetCoursePreviewDto> { IsSuccess = false, ErrorMessage = "Course with the provided ID not found." };
            }
        }

        public async Task<Result<GetCourseDto>> GetCourse(int id)
        {
            try
            {
                var dbCourse = await _context.Courses
                    .Include(c => c.CourseLessons).ThenInclude(cl => cl.Lesson)
                        .ThenInclude(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                    .Include(c => c.CourseLessons).ThenInclude(cl => cl.Lesson)
                        .ThenInclude(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                    .FirstAsync(l => l.Id == id);

                var course = _mapper.Map<GetCourseDto>(dbCourse);
                return new Result<GetCourseDto> { IsSuccess = true, Data = course };
            }
            catch (System.Exception)
            {
                return new Result<GetCourseDto> { IsSuccess = false, ErrorMessage = "Course with the provided ID not found." };
            }
        }
    }
}