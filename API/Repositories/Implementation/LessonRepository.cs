using System.Text.RegularExpressions;
using API.Data;
using API.Dtos.Lesson;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        private readonly VideoService _videoService;
        private readonly UserManager<User> _userManager;
        private readonly ICoursesRepository _coursesRepository;
        public LessonsRepository(
            CourseContext context,
            UserManager<User> userManager,
            ICoursesRepository coursesRepository,
            IMapper mapper,
            VideoService videoService)
        {
            _userManager = userManager;
            _coursesRepository = coursesRepository;
            _context = context;
            _mapper = mapper;
            _videoService = videoService;
        }

        public async Task<Result<List<GetLessonDto>>> GetLessons()
        {
            var dbLessons = _context.Lessons
                .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson);

            var lessons = await dbLessons.Select(l => _mapper.Map<GetLessonDto>(l)).ToListAsync();
            return new Result<List<GetLessonDto>> { IsSuccess = true, Data = lessons };
        }

        public async Task<Result<GetLessonDto>> GetLesson(int id, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            try
            {
                var dbLesson = await _context.Lessons
                    .Include(l => l.LessonKeywords).ThenInclude(lk => lk.Keyword)
                    .Include(l => l.PreviousLessons).ThenInclude(lpl => lpl.PreviousLesson)
                    .Include(l => l.UserLessons)
                    .FirstAsync(l => l.Id == id);

                var lesson = _mapper.Map<GetLessonDto>(dbLesson, opts => opts.Items["UserId"] = user?.Id);
                return new Result<GetLessonDto> { IsSuccess = true, Data = lesson };
            }
            catch (System.Exception)
            {
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };
            }
        }

        public async Task<Result<GetLessonDto>> AddLesson(AddLessonDto newLesson, string username)
        {
            var lesson = _mapper.Map<Lesson>(newLesson);

            var theoryResult = await ProcessVideoForLesson(newLesson.TheoryFile, null, (url, id) => { lesson.UrlTheory = url; lesson.TheoryPublicId = id; });
            if (theoryResult != null) return theoryResult;

            var practiceResult = await ProcessVideoForLesson(newLesson.PracticeFile, null, (url, id) => { lesson.UrlPractice = url; lesson.PracticePublicId = id; });
            if (practiceResult != null) return practiceResult;

            lesson.Id = _context.Lessons.Max(l => l.Id) + 1;
            await _context.Lessons.AddAsync(lesson);
            var isSuccess = await _context.SaveChangesAsync() > 0;

            if (isSuccess) return new Result<GetLessonDto> { IsSuccess = true, Data = GetLesson(lesson.Id, username).Result.Data };

            return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "New lesson can not be saved." };
        }

        public async Task<Result<GetLessonDto>> UpdateLesson(int id, UpdateLesssonDto updatedLesson, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Unauthorized user." };

            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                var isLessonAvailable = await IsLessonAvailaible(id, updatedLesson, username);
                if (isLessonAvailable != null) return isLessonAvailable;
            }

            var dbLesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);

            if (dbLesson == null) return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };

            dbLesson.Title = updatedLesson.Title ?? dbLesson.Title;
            dbLesson.Description = updatedLesson.Description ?? dbLesson.Description;
            dbLesson.Number = updatedLesson.Number != -1 ? updatedLesson.Number : dbLesson.Number;
            dbLesson.Importance = updatedLesson.Importance != -1 ? updatedLesson.Importance : dbLesson.Importance;

            var theoryResult = await ProcessVideoForLesson(updatedLesson.TheoryFile, dbLesson.TheoryPublicId, (url, id) => { dbLesson.UrlTheory = url; dbLesson.TheoryPublicId = id; });
            if (theoryResult != null) return theoryResult;

            var practiceResult = await ProcessVideoForLesson(updatedLesson.PracticeFile, dbLesson.PracticePublicId, (url, id) => { dbLesson.UrlPractice = url; dbLesson.PracticePublicId = id; });
            if (practiceResult != null) return practiceResult;

            if (user != null)
            {
                UserLesson userLesson = await _context.UserLessons
                    .Where(sl => sl.LessonId == id).FirstOrDefaultAsync(sl => sl.UserId == user.Id);

                if (userLesson == null)
                {
                    _context.UserLessons.Add(
                        new UserLesson
                        {
                            LessonId = id,
                            UserId = user.Id,
                            IsTheoryCompleted = updatedLesson.IsTheoryCompleted != -1 && (updatedLesson.IsTheoryCompleted != 0),
                            IsPracticeCompleted = updatedLesson.IsPracticeCompleted != -1 && (updatedLesson.IsPracticeCompleted != 0),
                            TestScore = updatedLesson.TestScore != -1 ? updatedLesson.TestScore : null,
                        });
                }
                else
                {
                    userLesson.IsTheoryCompleted = updatedLesson.IsTheoryCompleted != -1 ? (updatedLesson.IsTheoryCompleted != 0) : userLesson.IsTheoryCompleted;
                    userLesson.IsPracticeCompleted = updatedLesson.IsPracticeCompleted != -1 ? (updatedLesson.IsPracticeCompleted != 0) : userLesson.IsPracticeCompleted;
                    userLesson.TestScore = updatedLesson.TestScore != -1 ? updatedLesson.TestScore : userLesson.TestScore;
                }
            }

            var isSuccess = await _context.SaveChangesAsync() > 0;

            if (isSuccess) return new Result<GetLessonDto> { IsSuccess = true, Data = GetLesson(id, username).Result.Data };

            return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Updated lesson can not be saved." };

        }

        public async Task<Result<bool>> DeleteLesson(int id)
        {
            try
            {
                var dbLesson = await _context.Lessons.FirstAsync(l => l.Id == id);

                if (!string.IsNullOrEmpty(dbLesson.TheoryPublicId))
                    await _videoService.DeleteVideoAsync(dbLesson.TheoryPublicId);

                if (!string.IsNullOrEmpty(dbLesson.PracticePublicId))
                    await _videoService.DeleteVideoAsync(dbLesson.PracticePublicId);

                _context.Lessons.Remove(dbLesson);
                await _context.SaveChangesAsync();

                return new Result<bool> { IsSuccess = true };
            }
            catch (System.Exception)
            {
                return new Result<bool> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID not found." };
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
        private async Task<Result<GetLessonDto>> ProcessVideoForLesson(IFormFile file, string existingPublicId, Action<string, string> updateLesson)
        {
            if (file != null)
            {
                if (!string.IsNullOrEmpty(existingPublicId))
                    await _videoService.DeleteVideoAsync(existingPublicId);

                var videoResult = await _videoService.AddVideoAsync(file);

                if (videoResult.Error != null)
                    return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = videoResult.Error.Message };

                updateLesson(videoResult.SecureUrl.ToString(), videoResult.PublicId);
            }
            return null;
        }

        private async Task<Result<GetLessonDto>> IsLessonAvailaible(int id, UpdateLesssonDto updatedLesson, string username)
        {
            if (updatedLesson.courseId == -1)
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "You should provide course ID." };

            var courseResult = await _coursesRepository.GetCourse(updatedLesson.courseId, null, null, "", username);
            if (!courseResult.IsSuccess)
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = courseResult.ErrorMessage };

            var section = courseResult.Data.Sections.FirstOrDefault(s => s.Lessons.Any(l => l.Id == id));
            if (section == null)
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson with the provided ID was not found in any of sections." };

            var isLessonAvailable = section?.IsAvailable;
            if (!isLessonAvailable.HasValue || !isLessonAvailable.Value)
                return new Result<GetLessonDto> { IsSuccess = false, ErrorMessage = "Lesson is not available." };

            return null;
        }
    }
}