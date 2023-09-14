# Web App for maths course

## About project

This is a Web App for taking the math courses. For now, here is a web API that helps you interact with a database that stores math courses data, and the client side to take courses (log in, see which lessons have already been completed, choose next lesson to complete, watch videos, take tests and see the result).

More about business logic, database schema, component diagram you can see in [design document](https://docs.google.com/document/d/1bEvHXDxrGMU5eWxjBdT6eoA5OIkL18bKe9ypRxkODgo/edit?usp=sharing).

To run the project and try by yourself see "How to run".

## Design document

[here](https://docs.google.com/document/d/1bEvHXDxrGMU5eWxjBdT6eoA5OIkL18bKe9ypRxkODgo/edit?usp=sharing)

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

## Web API

If you want to test how the Web API works it would be enough to run only API folder (see "How to run").

- then open [http://localhost:5000/swagger](http://localhost:5000/swagger) in your browser and try to use some endpoints to get or change data in database.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/2400f4a2-5c6c-4426-b99f-149a3a33687c' width='700'>

## Examples

### Web App
To see what the courses look like inside, you should log in.

> Use a demo account:
> - username: bob
> - password: Pa$$w0rd

>There won't be any confidential information about the courses here, so that's fine.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/91527a47-f2d8-4906-8c57-080c72a2c09d)

Now, you have access to some sections in 1st course. Open it by selecting "Курси" tab on the top bar and choose "Дізнатись більше". 
![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/0757c31e-ce2d-4621-8c7c-bc2962a95a7b)

Here you can see what stage each lesson is at (completed, in progress, not started) and how many completed lessons are in each section.
![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/3b98855b-e1fb-4ba2-9256-d8a3acf6b06f)

Then you can try how filters work and choose the lesson you want and start passing it.
![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/3a9f6766-a572-47af-ac49-ca322640057a)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/9d9d1c94-140b-4d21-815f-07226ece4be9)

Also you can take the tests.
![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/4d59ae85-bad7-4849-b6d2-6a39b86f78e8)

After answering, you immediately see whether you are right or not.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/bcd44b5f-3255-40d9-af06-d8d230d8aee1' width='400'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/157fbc60-968f-4134-a1fe-125c177fa984' width='400'>

And of course you see your score at the end.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/52f0238e-2851-4aad-bea6-325eff95f04f' width='500'>

### Web API
Example of getting _lesson by id_, where you can see information about the lesson, as well as a list of keywords and lessons that are recommended to be completed/repeated before.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/24695d81-2e8a-43f6-8418-4db3fcb89956' width='700'>

### Tests
If you want to **run tests** (you also can see that all tests are passed in [_Github Actions_](https://github.com/yaryna-bashchak/maths-course/actions))

- go back to the root folder maths-course if you are not there
- run the following command

<code>$ dotnet test ./API.Tests</code>

Example:

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/c05c51ec-8fde-42bd-9022-387070f7b4c3)
