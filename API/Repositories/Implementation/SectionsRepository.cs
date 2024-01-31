using API.Controllers;
using API.Data;
using API.Dtos.Section;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class SectionsRepository : ISectionsRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public SectionsRepository(
            CourseContext context,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<GetSectionDto>> GetSection(int id)
        {
            try
            {
                var dbSection = await _context.Sections
                    .Include(s => s.SectionLessons).ThenInclude(sl => sl.Lesson)
                    .FirstAsync(l => l.Id == id);

                var section = _mapper.Map<GetSectionDto>(dbSection);
                return new Result<GetSectionDto> { IsSuccess = true, Data = section };
            }
            catch (System.Exception)
            {
                return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = "Section with the provided ID not found." };
            }
        }

        public async Task<Result<GetSectionDto>> UpdateSectionAvailability(int sectionId, UpdateUserSectionDto updatedUserSection, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return UnauthorizedResult("Unauthorized user.");

            var dbSection = _context.Sections.Find(sectionId);
            if (dbSection == null) return NotFoundResult("Section with the provided ID not found.");

            if (updatedUserSection.IsAvailable != -1)
            {
                UserSection userSection = await _context.UserSections
                    .Where(sl => sl.SectionId == sectionId)
                    .FirstOrDefaultAsync(sl => sl.UserId == user.Id);

                if (userSection == null)
                {
                    _context.UserSections.Add(
                        new UserSection
                        {
                            SectionId = sectionId,
                            UserId = user.Id,
                            isAvailable = updatedUserSection.IsAvailable != 0,
                        });
                }
                else
                {
                    userSection.isAvailable = updatedUserSection.IsAvailable != 0;
                }
            }

            await _context.SaveChangesAsync();

            return new Result<GetSectionDto> { IsSuccess = true };
        }

        public async Task<Result<GetSectionDto>> AddSection(AddSectionDto newSection)
        {
            var section = _mapper.Map<Section>(newSection);

            section.Id = _context.Sections.Max(c => c.Id) + 1;
            await _context.Sections.AddAsync(section);

            return await SaveChangesAndReturnResult(section.Id);
        }

        public async Task<Result<GetSectionDto>> UpdateSection(int id, UpdateSectionDto updatedSection)
        {
            try
            {
                var dbSection = _context.Sections.Find(id);

                if (dbSection == null) return NotFoundResult("Section with the provided ID not found.");

                dbSection.Title = updatedSection.Title ?? dbSection.Title;
                dbSection.Description = updatedSection.Description ?? dbSection.Description;
                dbSection.Number = updatedSection.Number != -1 ? updatedSection.Number : dbSection.Number;

                if (updatedSection.LessonIdsToAdd != null)
                {
                    foreach (var lessonId in updatedSection.LessonIdsToAdd)
                    {
                        Lesson lesson = await _context.Lessons
                            .FirstOrDefaultAsync(l => l.Id == lessonId);

                        if (lesson == null)
                        {
                            return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = "Lesson with ID " + lessonId + "not found." };
                        }

                        SectionLesson sectionLesson = await _context.SectionLessons
                            .Where(sl => sl.SectionId == id).FirstOrDefaultAsync(sl => sl.LessonId == lessonId);

                        if (sectionLesson != null)
                        {
                            return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = "Connection between Section with ID " + id + " and Lesson with ID " + lessonId + " already exists." };
                        }

                        _context.SectionLessons.Add(
                            new SectionLesson
                            {
                                SectionId = id,
                                LessonId = lessonId,
                            }
                        );
                    }
                }

                if (updatedSection.LessonIdsToDelete != null)
                {
                    foreach (var lessonId in updatedSection.LessonIdsToDelete)
                    {
                        Lesson lesson = await _context.Lessons
                            .FirstOrDefaultAsync(l => l.Id == lessonId);

                        if (lesson == null)
                        {
                            return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = "Lesson with ID " + lessonId + "not found." };
                        }

                        SectionLesson sectionLesson = await _context.SectionLessons
                            .Where(sl => sl.SectionId == id).FirstOrDefaultAsync(sl => sl.LessonId == lessonId);

                        if (sectionLesson == null)
                        {
                            return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = "Connection between Section with ID " + id + " and Lesson with ID" + lessonId + "not found." };
                        }

                        _context.SectionLessons.Remove(sectionLesson);
                    }
                }

                await _context.SaveChangesAsync();

                var result = await GetSection(id);
                return new Result<GetSectionDto> { IsSuccess = true, Data = result.Data };
            }
            catch (System.Exception ex)
            {
                return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<Result<bool>> DeleteSection(int id)
        {
            try
            {
                var dbSection = await _context.Sections.FirstOrDefaultAsync(s => s.Id == id);

                if (dbSection == null) return new Result<bool> { IsSuccess = false, ErrorMessage = "Section with the provided ID not found." };

                _context.Sections.Remove(dbSection);
                await _context.SaveChangesAsync();

                return new Result<bool> { IsSuccess = true, Data = true };
            }
            catch (System.Exception ex)
            {
                return new Result<bool> { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        private async Task<Result<GetSectionDto>> SaveChangesAndReturnResult(int sectionId)
        {
            try
            {
                await _context.SaveChangesAsync();
                return new Result<GetSectionDto> { IsSuccess = true, Data = GetSection(sectionId).Result.Data };
            }
            catch (System.Exception ex)
            {
                return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        private Result<GetSectionDto> UnauthorizedResult(string errorMessage)
        {
            return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = errorMessage };
        }

        private Result<GetSectionDto> NotFoundResult(string errorMessage)
        {
            return new Result<GetSectionDto> { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}