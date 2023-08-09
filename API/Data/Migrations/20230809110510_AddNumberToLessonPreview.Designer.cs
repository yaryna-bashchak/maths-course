﻿// <auto-generated />
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(CourseContext))]
    [Migration("20230809110510_AddNumberToLessonPreview")]
    partial class AddNumberToLessonPreview
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("API.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PriceFull")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PriceMonthly")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("API.Entities.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Word")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("API.Entities.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Importance")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPracticeCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsTheoryCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TestScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlPractice")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlTheory")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("API.Entities.LessonKeyword", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("KeywordId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LessonId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("LessonKeywords");
                });

            modelBuilder.Entity("API.Entities.LessonPreviousLesson", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PreviousLessonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LessonId", "PreviousLessonId");

                    b.HasIndex("PreviousLessonId");

                    b.ToTable("PreviousLessons");
                });

            modelBuilder.Entity("API.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isAnswer")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("API.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("API.Entities.SectionLesson", b =>
                {
                    b.Property<int>("SectionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SectionId", "LessonId");

                    b.HasIndex("LessonId");

                    b.ToTable("SectionLessons");
                });

            modelBuilder.Entity("API.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Question")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("API.Entities.LessonKeyword", b =>
                {
                    b.HasOne("API.Entities.Keyword", "Keyword")
                        .WithMany("LessonKeywords")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Lesson", "Lesson")
                        .WithMany("LessonKeywords")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Keyword");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("API.Entities.LessonPreviousLesson", b =>
                {
                    b.HasOne("API.Entities.Lesson", "Lesson")
                        .WithMany("PreviousLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Lesson", "PreviousLesson")
                        .WithMany("PreviousLessonOf")
                        .HasForeignKey("PreviousLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("PreviousLesson");
                });

            modelBuilder.Entity("API.Entities.Option", b =>
                {
                    b.HasOne("API.Entities.Test", "Test")
                        .WithMany("Options")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("API.Entities.Section", b =>
                {
                    b.HasOne("API.Entities.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("API.Entities.SectionLesson", b =>
                {
                    b.HasOne("API.Entities.Lesson", "Lesson")
                        .WithMany("SectionLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Section", "Section")
                        .WithMany("SectionLessons")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("API.Entities.Test", b =>
                {
                    b.HasOne("API.Entities.Lesson", "Lesson")
                        .WithMany("Tests")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("API.Entities.Course", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("API.Entities.Keyword", b =>
                {
                    b.Navigation("LessonKeywords");
                });

            modelBuilder.Entity("API.Entities.Lesson", b =>
                {
                    b.Navigation("LessonKeywords");

                    b.Navigation("PreviousLessonOf");

                    b.Navigation("PreviousLessons");

                    b.Navigation("SectionLessons");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("API.Entities.Section", b =>
                {
                    b.Navigation("SectionLessons");
                });

            modelBuilder.Entity("API.Entities.Test", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
