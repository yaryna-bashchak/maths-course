using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Lesson;
using API.Entities;
using AutoMapper;

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

        public List<GetLessonDto> AddLesson(AddLessonDto newLesson)
        {
            var lesson = _mapper.Map<Lesson>(newLesson);
            lesson.Id = _context.Lessons.Max(l => l.Id) + 1;
            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return _context.Lessons.Select(l => _mapper.Map<GetLessonDto>(l)).ToList();
        }
    }
}