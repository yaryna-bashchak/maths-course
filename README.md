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
- run the following command to install all needed packages

<code>$ dotnet restore </code>

Now you're ready to start.
If you want to **run the project**:

- go to API folder with command and run project with the following commands:

<code>$ cd API</code></br>
<code>$ dotnet run</code>
- then open [http://localhost:5000/swagger](http://localhost:5000/swagger) in your browser
- and try to use some endpoints to get or change data in database (it's better to start with the _lesson controller_, because it is the most interesting)

Example of getting _lesson by id_, where you can see information about the lesson, as well as a list of keywords and lessons that are recommended to be completed/repeated before

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/24695d81-2e8a-43f6-8418-4db3fcb89956)

or example of searching _lessons that have given keyword pattern_:

if you try to write "теор" it will find lessons that have keywords such as "теорема Піфагора", "теорема Вієта", "теорема косинусів"

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/09077d96-1db4-47b6-834f-544087d72018)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/05c3ea4c-c4b2-43c0-a602-94fdf3e17339)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/052b19f8-a4fd-47ac-8c59-fa013af74176)

Or if you want to **run tests**

- go back to the root folder maths-course if you are not there
- run the following command

<code>$ dotnet test ./API.Tests</code>

Example:

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/c05c51ec-8fde-42bd-9022-387070f7b4c3)

---
