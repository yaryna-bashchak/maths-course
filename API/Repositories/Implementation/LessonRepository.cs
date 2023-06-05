using System.Text.RegularExpressions;
using API.Data;
using API.Dtos.Lesson;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class LessonsRepository : ILessonsRepository
    {
        private CourseContext _context;
        private IMapper _mapper;
        public LessonsRepository(
            CourseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<GetLessonDto>>> GetLessons()
        {
            var dbLessons = _context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson);

            var lessons = await dbLessons.Select(l => _mapper.Map<GetLessonDto>(l)).ToListAsync();
            return new Result<List<GetLessonDto>> { IsSuccess = true, Data = lessons };
        }

        public async Task<Result<GetLessonDto>> GetLesson(int id)
        {
            try
            {
                var dbLesson = await _context.Lessons
                    .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                    .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                    .FirstAsync(l => l.Id == id);

                var lesson = _mapper.Map<GetLessonDto>(dbLesson);
                return new Result<GetLessonDto> { IsSuccess = true, Data = lesson };
            }
            catch (System.Exception)
            {
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };
            }
        }

        public async Task<Result<List<GetLessonDto>>> AddLesson(AddLessonDto newLesson)
        {
            var lesson = _mapper.Map<Lesson>(newLesson);
            lesson.Id = _context.Lessons.Max(l => l.Id) + 1;
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();

            var result = await GetLessons();
            return new Result<List<GetLessonDto>> { IsSuccess = true, Data = result.Data };
        }

        public async Task<Result<GetLessonDto>> UpdateLesson(UpdateLesssonDto updatedLesson)
        {
            try
            {
                var dbLesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == updatedLesson.Id);

                dbLesson.Title = updatedLesson.Title ?? dbLesson.Title;
                dbLesson.Description = updatedLesson.Description ?? dbLesson.Description;
                dbLesson.UrlTheory = updatedLesson.UrlTheory ?? dbLesson.UrlTheory;
                dbLesson.UrlPractice = updatedLesson.UrlPractice ?? dbLesson.UrlPractice;
                dbLesson.Number = updatedLesson.Number != -1 ? updatedLesson.Number : dbLesson.Number;
                dbLesson.Importance = updatedLesson.Importance != -1 ? updatedLesson.Importance : dbLesson.Importance;
                dbLesson.isCompleted = updatedLesson.IsCompleted != -1 ? (updatedLesson.IsCompleted != 0) : dbLesson.isCompleted;
                await _context.SaveChangesAsync();

                var result = await GetLesson(updatedLesson.Id);
                return new Result<GetLessonDto> { IsSuccess = true, Data = result.Data };
            }
            catch (System.Exception)
            {
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };
            }
        }

        public async Task<Result<List<GetLessonDto>>> DeleteLesson(int id)
        {
            try
            {
                var dbLesson = await _context.Lessons.FirstAsync(l => l.Id == id);

                _context.Lessons.Remove(dbLesson);
                await _context.SaveChangesAsync();

                var result = await GetLessons();
                return new Result<List<GetLessonDto>> { IsSuccess = true, Data = result.Data };
            }
            catch (System.Exception)
            {
                return new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };
            }
        }

        public async Task<Result<List<GetLessonDto>>> GetLessonsByKeyword(string keyword)
        {
            var regex = new Regex(keyword, RegexOptions.IgnoreCase);

            var dbLessons = await _context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                .ToListAsync();

            var lessons = dbLessons
                .Select(l => _mapper.Map<GetLessonDto>(l))
                .Where(l => l.Keywords.Any(lk => regex.IsMatch(lk.Word)))
                .ToList();

            if (lessons == null || !lessons.Any())
            {
                return new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Lessons with this keyword pattern not found." };
            }

            return new Result<List<GetLessonDto>> { IsSuccess = true, Data = lessons };
        }

        public async Task<Result<List<GetLessonDto>>> GetLessonsByKeywordID(int id)
        {
            var dbLessons = _context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                .Where(l => l.LessonKeywords.Any(lk => lk.Keyword.Id == id));

            var lessons = await dbLessons
                .Select(l => _mapper.Map<GetLessonDto>(l)).ToListAsync();

            if (lessons == null || !lessons.Any())
            {
                return new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Lessons with this keyword ID not found." };
            }

            return new Result<List<GetLessonDto>> { IsSuccess = true, Data = lessons };
        }

        public async Task<Result<List<GetLessonDto>>> GetLessonsByImportance(int importance)
        {
            if (importance < 0)
            {
                return new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "Importance cannot be less than 0 (it can only be 0, 1 or 2)." };
            }

            var dbLessons = _context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                .Where(l => l.Importance <= importance);

            var lessons = await dbLessons.Select(l => _mapper.Map<GetLessonDto>(l)).ToListAsync();

            if (lessons == null || !lessons.Any())
            {
                return new Result<List<GetLessonDto>> { IsSuccess = false, ErrorMessage = "It seems there are no lessons whose importance is equal to or less than given one (it can only be 0, 1 or 2)." };
            }

            return new Result<List<GetLessonDto>> { IsSuccess = true, Data = lessons };
        }
    }
}