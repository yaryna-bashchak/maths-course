using API.Data;
using API.Dtos.Course;
using API.Dtos.Lesson;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public CoursesRepository(
            CourseContext context,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<GetCourseDto>> GetCourse(int id, int? maxImportance, bool? onlyUncompleted, string searchTerm, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            try
            {
                var dbCourse = await _context.Courses
                    .Include(c => c.Sections).ThenInclude(s => s.SectionLessons).ThenInclude(sl => sl.Lesson)
                        .ThenInclude(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                    .Include(c => c.Sections).ThenInclude(s => s.SectionLessons).ThenInclude(sl => sl.Lesson)
                        .ThenInclude(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                    .Include(c => c.Sections).ThenInclude(s => s.UserSections)
                    .FirstAsync(l => l.Id == id);

                var course = _mapper.Map<GetCourseDto>(dbCourse, opts => opts.Items["UserId"] = user?.Id)
                    .SortAndRenumberLessons()
                    .Filter(maxImportance, onlyUncompleted)
                    .Search(searchTerm);

                if (user == null)
                {
                    foreach (var section in course.Sections)
                    {
                        var lessonPreviews = _mapper.Map<List<GetLessonPreviewDto>>(section.Lessons);
                        var lessons = _mapper.Map<List<GetLessonDto>>(lessonPreviews);
                        section.Lessons = lessons;
                    }
                }

                return new Result<GetCourseDto> { IsSuccess = true, Data = course };
            }
            catch (System.Exception)
            {
                return new Result<GetCourseDto> { IsSuccess = false, ErrorMessage = "Course with the provided ID not found." };
            }
        }

        public async Task<Result<GetCoursePreviewDto>> GetCoursePreview(int id)
        {
            try
            {
                var dbCourse = await _context.Courses
                    .Include(c => c.Sections).ThenInclude(s => s.SectionLessons).ThenInclude(sl => sl.Lesson)
                    .FirstAsync(l => l.Id == id);

                var course = _mapper.Map<GetCoursePreviewDto>(dbCourse)
                    .SortAndRenumberLessons();

                return new Result<GetCoursePreviewDto> { IsSuccess = true, Data = course };
            }
            catch (System.Exception)
            {
                return new Result<GetCoursePreviewDto> { IsSuccess = false, ErrorMessage = "Course with the provided ID not found." };
            }
        }

        public async Task<Result<List<GetCoursePreviewDto>>> GetCourses()
        {
            try
            {
                var dbCourses = _context.Courses;

                var courses = await dbCourses.Select(c => _mapper.Map<GetCoursePreviewDto>(c)).ToListAsync();
                return new Result<List<GetCoursePreviewDto>> { IsSuccess = true, Data = courses };
            }
            catch (System.Exception)
            {
                return new Result<List<GetCoursePreviewDto>> { IsSuccess = false, ErrorMessage = "Courses not found." };
            }
        }
    }
}