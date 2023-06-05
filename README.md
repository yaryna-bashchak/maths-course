# Web API for maths course
### Project structure

```
Maths course
│
├───API
│   │   API.csproj
│   │   AutoMapperProfile.cs
│   │   course.db
│   │   Program.cs
│   │
│   ├───Controllers
│   │       KeywordsController.cs
│   │       LessonKeywordController.cs
│   │       LessonsController.cs
│   │
│   ├───Data
│   │   │   CourseContext.cs
│   │   │   DbInitializer.cs
│   │   │
│   │   └───Migrations
│   │           ...
│   │
│   ├───Dtos
│   │   ├───Keyword
│   │   │       AddKeywordDto.cs
│   │   │       GetKeywordDto.cs
│   │   │
│   │   ├───Lesson
│   │   │       AddLessonDto.cs
│   │   │       GetLessonDto.cs
│   │   │       GetPreviousLessonDto.cs
│   │   │       UpdateLesssonDto.cs
│   │   │
│   │   └───LessonKeyword
│   │           AddLessonKeywordDto.cs
│   │
│   ├───Entities
│   │       Course.cs
│   │       CourseLesson.cs
│   │       Keyword.cs
│   │       Lesson.cs
│   │       LessonKeyword.cs
│   │       LessonPreviousLesson.cs
│   │       Option.cs
│   │       Test.cs
│   │
│   └───Repositories
│       │   ILessonsRepository.cs
│       │   Result.cs
│       │
│       └───Implementation
│               LessonRepository.cs
│
└───API.Tests
│   │   API.Tests.csproj
│   │   LessonsControllerTests.cs
│   │   Usings.cs
│
└─── <others>

```
 
---

### About project

For now, this is a web API that helps you interact with a database that stores math course data. You can see the database schema and component diagram below.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/55c18a95-a191-48f2-a582-b27c37891ed9)

![Component diagram drawio (1)](https://github.com/yaryna-bashchak/maths-course/assets/90560209/c16c4614-197e-439e-9cf1-cc4d97c4b648)

This project implements functions for the user (student) for convenient course completion, such as displaying of lessons that should be completed/repeated before that, division of lessons into 3 levels of difficulty, ability to filter lessons by category and so on.

Try to run example of usage (see "How to run").

---

### How to run
- first of all, you need to install [.Net 7.0](https://dotnet.microsoft.com/en-us/download) (if you do not already have it)
- open this folder in [VS Code](https://code.visualstudio.com/download)
- install _C# extension_ (if you do not already have it)
<img src="https://user-images.githubusercontent.com/90560209/220602921-cdde3e17-3c4b-458c-bfd0-d0bf2fdd2529.png" height="80">

- write next commands in Terminal if you want to **run the program**

<code>$ cd API </code>

<code>$ dotnet run </code>

- or next if you want to **run tests** (if you have trobles with running the tests do steps below)

<code>$ dotnet test ./lab2_tests</code>

#### If you have errors when running the tests, it's probably because you don't have the NUnit packages installed.

Follow the next steps:

- install _NuGet Gallery_ extention (if you do not already have it)

<img src="https://user-images.githubusercontent.com/90560209/226548216-92ceeca0-8f9a-4e59-a023-e5f2713fe34b.png" height="80">

- open NuGet Gallery

![image](https://user-images.githubusercontent.com/90560209/226548521-a836523e-d625-4046-89d5-ee967a23aa14.png)

- find and install next packages (choose install only for **lab2_tests.csproj**)

NUnit

<img src="https://user-images.githubusercontent.com/90560209/226549367-db191e82-aeb9-4a6b-a367-799df4bbeb8b.png" height="150"> <img src="https://user-images.githubusercontent.com/90560209/226549385-1a788edf-1b63-4f34-91a3-8bb412ee1e60.png" height="150">

- try to run tests again by command

<code>$ dotnet test ./lab2_tests</code>

---
