# Web App for maths course
 
---

## About project

This is a Web App for the maths course. For now, here is a web API that helps you interact with a database that stores math course data, and the client side to take courses (see which lessons have already been completed, choose next lesson to complete, watch videos, take tests and see the result).

More about business logic, database schema, component diagram you can see in [design document](https://docs.google.com/document/d/1bEvHXDxrGMU5eWxjBdT6eoA5OIkL18bKe9ypRxkODgo/edit?usp=sharing).

To run the project and try by yourself see "How to run".

---

## Design document

[here](https://docs.google.com/document/d/1bEvHXDxrGMU5eWxjBdT6eoA5OIkL18bKe9ypRxkODgo/edit?usp=sharing)

---

## How to run
- first of all, you need to install [.Net 7.0](https://dotnet.microsoft.com/en-us/download) (if you do not already have it)
- open this folder in [VS Code](https://code.visualstudio.com/download)
- run the following command to install all needed packages

<code>$ dotnet restore </code>

Now you're ready to start.
If you want to **run the app**:

- go to API folder and run it using the following commands:

<code>$ cd API</code></br>
<code>$ dotnet run</code>

- then open second terminal and run client side using the following commands:

<code>$ cd client</code></br>
<code>$ npm start</code>

After that, the home page of the app will be opened in your browser.

---

### Web API

If you want to test how the Web API works it would be enough to run only API folder (see "How to run").

- then open [http://localhost:5000/swagger](http://localhost:5000/swagger) in your browser and try to use some endpoints to get or change data in database.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/2400f4a2-5c6c-4426-b99f-149a3a33687c)

---

### Examples

#### Web API
Example of getting _lesson by id_, where you can see information about the lesson, as well as a list of keywords and lessons that are recommended to be completed/repeated before

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/24695d81-2e8a-43f6-8418-4db3fcb89956)

or example of searching _lessons that have given keyword pattern_:

if you try to write "теор" it will find lessons that have keywords such as "теорема Піфагора", "теорема Вієта", "теорема косинусів"

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/09077d96-1db4-47b6-834f-544087d72018)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/05c3ea4c-c4b2-43c0-a602-94fdf3e17339)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/052b19f8-a4fd-47ac-8c59-fa013af74176)

#### Tests

If you want to **run tests** (you also can see that all tests are passed in [_Github Actions_](https://github.com/yaryna-bashchak/maths-course/actions))

- go back to the root folder maths-course if you are not there
- run the following command

<code>$ dotnet test ./API.Tests</code>

Example:

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/c05c51ec-8fde-42bd-9022-387070f7b4c3)
