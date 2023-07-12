# Web App for maths course
 
---

### About project

For now, this is a web API that helps you interact with a database that stores math course data.

This project implements functions for the user (student) for convenient course completion, such as displaying of lessons that should be completed/repeated before that, division of lessons into 3 levels of difficulty, ability to filter lessons by category and so on.

More about business logic, database schema, component diagram you can see in [design document](https://docs.google.com/document/d/1Qa3eTDBOGj27OgakiaHTJO45HmPfYqd6rW9GfFrkzkg/edit?usp=sharing).

To run the project and try by yourself see "How to run".

---

### Design document

[here](https://docs.google.com/document/d/1bEvHXDxrGMU5eWxjBdT6eoA5OIkL18bKe9ypRxkODgo/edit?usp=sharing)

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

Or if you want to **run tests** (you also can see that all tests are passed in [_Github Actions_](https://github.com/yaryna-bashchak/maths-course/actions))

- go back to the root folder maths-course if you are not there
- run the following command

<code>$ dotnet test ./API.Tests</code>

Example:

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/c05c51ec-8fde-42bd-9022-387070f7b4c3)
