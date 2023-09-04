using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Section;
using API.Dtos.Keyword;
using API.Dtos.Lesson;
using API.Dtos.LessonKeyword;
using API.Dtos.Option;
using API.Dtos.Test;
using API.Entities;
using AutoMapper;
using API.Dtos.Course;

namespace API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Lesson, GetLessonDto>()
                .ForMember(dto => dto.Keywords, l => l.MapFrom(l => l.LessonKeywords.Select(lk => lk.Keyword)))
                .ForMember(dto => dto.PreviousLessons, l => l.MapFrom(l => l.PreviousLessons.Select(lpl => lpl.PreviousLesson)))
                .AfterMap((src, dest, context) =>
                {
                    var userId = context.Items["UserId"] as string;
                    var userLesson = src.UserLessons.FirstOrDefault(ul => ul.UserId == userId);
                    if (userLesson != null)
                    {
                        dest.IsTheoryCompleted = userLesson.IsTheoryCompleted;
                        dest.IsPracticeCompleted = userLesson.IsPracticeCompleted;
                        dest.TestScore = userLesson.TestScore;
                    }
                });
            CreateMap<Lesson, GetLessonPreviewDto>();
            CreateMap<AddLessonDto, Lesson>();
            CreateMap<Lesson, GetPreviousLessonDto>();
            CreateMap<GetLessonDto, GetLessonPreviewDto>();
            CreateMap<GetLessonPreviewDto, GetLessonDto>();

            CreateMap<Keyword, GetKeywordDto>();
            CreateMap<AddKeywordDto, Keyword>();

            CreateMap<AddLessonKeywordDto, LessonKeyword>();
            CreateMap<LessonKeyword, AddLessonKeywordDto>();

            CreateMap<Test, GetTestDto>()
                .ForMember(dto => dto.Options, t => t.MapFrom(t => t.Options));
            CreateMap<Option, GetOptionDto>();

            CreateMap<AddTestDto, Test>();
            CreateMap<AddOptionDto, Option>();

            CreateMap<Section, GetSectionDto>()
                .ForMember(dto => dto.Lessons, opt => opt.MapFrom(s => s.SectionLessons.Select(sl => sl.Lesson)))
                .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom((src, _, _, context) =>
                {
                    var userId = context.Items["UserId"] as string;
                    var userSection = src.UserSections.FirstOrDefault(us => us.UserId == userId);
                    return userSection?.isAvailable ?? false;
                }));
            CreateMap<Section, GetSectionPreviewDto>()
                .ForMember(dto => dto.Lessons, opt => opt.MapFrom(s => s.SectionLessons.Select(sl => sl.Lesson)));

            CreateMap<Course, GetCourseDto>()
                .ForMember(dto => dto.Sections, opt => opt.MapFrom(c => c.Sections));
            CreateMap<Course, GetCoursePreviewDto>()
                .ForMember(dto => dto.Sections, opt => opt.MapFrom(c => c.Sections));




            // CreateMap<SectionLesson, GetLessonDto>()
            //     .IncludeMembers(cl => cl.Lesson)
            //     .ForMember(dto => dto.MonthNumber, opt => opt.MapFrom(cl => cl.MonthNumber))
            //     .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom(cl => cl.IsAvailable));

            // CreateMap<Section, GetSectionDto>()
            //     .ForMember(dto => dto.Lessons, opt => opt.MapFrom(c => c.SectionLessons));

            // CreateMap<SectionLesson, GetLessonPreviewDto>()
            //     .IncludeMembers(cl => cl.Lesson)
            //     .ForMember(dto => dto.MonthNumber, opt => opt.MapFrom(cl => cl.MonthNumber))
            //     .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom(_ => false));
            // CreateMap<Section, GetSectionPreviewDto>()
            //     .ForMember(dto => dto.Lessons, opt => opt.MapFrom(c => c.SectionLessons));
            // CreateMap<Lesson, GetLessonPreviewDto>();
        }
    }
}