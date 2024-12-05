# Web App for Courses

## Technologies
- <img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/e3d0cafd-dcf6-4db2-9331-b8ca7b558d99' height='25'> **.NET** for the Back-end
- <img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/27b894e3-2717-4629-902d-3f46090a7502' height='25'> **React** for the Front-end
- <img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/e745a456-bd6a-4605-a679-9fd24fe14d36' height='25'> **Redux** for storing data from database on Front-end
- <img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/38fe52c2-2880-43e3-92be-1b9da5601e77' height='18'> **Cloudinary** server for storing videos and images

## About project

This is a Web App for creating and taking the courses. Here is a web API that helps you interact with a database that stores courses data, and the client side to create and take courses (log in, create and complete lessons, upload and watch videos, take tests).
 
<!---More about business logic, database schema, component diagram you can see in [design document](https://docs.google.com/document/d/1bEvHXDxrGMU5eWxjBdT6eoA5OIkL18bKe9ypRxkODgo/edit?usp=sharing). --->

[Deployed App](https://plan-znoshnika.fly.dev/course).

But to run the project yourself see "How to run".

## Examples

### 1) Taking the Course

#### - Create your own account, and "buy" courses.

<img width="824" alt="image" src="https://github.com/user-attachments/assets/e91a6ead-768b-4b30-b477-f28f4c83efdd">

Then you should see this message about successful registretion.

<img width="300" alt="image" src="https://github.com/user-attachments/assets/07b8a4a6-403f-4705-acde-7ce6bf51df7f">

Log in, go to the main page and "buy" some courses.

![image](https://github.com/user-attachments/assets/181569ef-fda7-48df-bfea-bab5398ac385)

Choose your plan.
![image](https://github.com/user-attachments/assets/ddc9b615-ceaf-4ece-b6b4-d83249ad1227)

Review purchase.
![image](https://github.com/user-attachments/assets/54de055d-cd5a-408f-8d67-45db12146e94)

Enter card data. Use the following test card data:

> card number: 4242 4242 4242 4242
> 
> expiry date: any future date
>
> CVV: any 3 digits

![image](https://github.com/user-attachments/assets/00d0bb0e-630d-42ee-bdd6-1ecf1093f6e3)

You should see the success message.

![image](https://github.com/user-attachments/assets/c143d29c-8073-496e-9421-978ebbbce88a)

#### - Complete some lessons.

Go the course.
![image](https://github.com/user-attachments/assets/cfef7612-6825-45c0-815b-53b21a889d5c)

Choose the lesson and start learning.

![image](https://github.com/user-attachments/assets/22380a13-3de6-49c1-bdaa-8c47b56adf04)

The lesson consists of videos and tests that should be completed after the lesson.

![image](https://github.com/user-attachments/assets/c857951d-abe0-4953-bd67-a2aae23aa4ca)

Complete the videos.

![image](https://github.com/user-attachments/assets/9ccc0880-b6ca-475f-a659-ecc9839f914b)

Pass the tests.
<img width="770" alt="image" src="https://github.com/user-attachments/assets/bdac0ff6-be3e-4739-9acd-f4982f7fcb12">


After answering, you immediately see whether you are right or not.


And of course you see your score at the end.


#### - Check your progress

Here you can see what stage each lesson is at (completed, in progress, not started) and how many completed lessons are in each section.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/ecdb5b69-16b3-49fc-8eb7-8cf1c24a994d)

Then you can try how filters work and choose the lesson you want and start passing it.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/dd8e2229-fab5-43f6-acc8-8adf5950f5eb)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/0cdbc4e0-dac2-4c7a-9fc5-621d912593ad)

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/ace81f53-29c3-4930-808c-e2130744a686)

Also you can take the tests.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/4d59ae85-bad7-4849-b6d2-6a39b86f78e8)

After answering, you immediately see whether you are right or not.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/bcd44b5f-3255-40d9-af06-d8d230d8aee1' width='400'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/157fbc60-968f-4134-a1fe-125c177fa984' width='400'>

And of course you see your score at the end.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/52f0238e-2851-4aad-bea6-325eff95f04f' width='500'>

### 2) Creating a Course

> Managing courses is allowed only to users with "Admin" role. They have "Редагувати курси" tab on the top bar.

Here you can create a new course or edit some.

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/de204bf4-dbe8-48f3-8edc-aafda6a6c4fc)

You can change any information, create new sections, lessons and so on.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/d40b8914-9642-4c33-8cee-bb4fdd512659' width='700'>

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/d4e7dfba-fcdb-457e-a9de-d12b25e59441' width='700'>

### Adaptive Design

The app is also adapted to smaller devices such as a phone or tablet. For example, for smaller screens, a side menu appears in the header, which repeats the same buttons that are on the large screens.
The content of the pages is also appropriate.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/b558a488-40e6-4d7f-9826-db64ae77e608' width='300'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/c233840c-e493-4daa-bcab-b0e6e2328c31' width='300'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/e44944ff-3285-4a14-a4dc-71de98179781' width='300'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/03be35e4-8e5f-43ec-93e7-7d63286adf73' width='300'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/7fcbce25-5814-4883-902c-9e8ca4eef35a' width='300'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/ab723440-a562-4b8c-8257-1c86e9d6e3a8' width='300'>

### Web API

If you want to test how the Web API works it would be enough to run only API folder (see "How to run").

- then open [http://localhost:5000/swagger](http://localhost:5000/swagger) in your browser and try to use some endpoints to get or change data in database.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/f9867e92-b506-4ea7-8096-152be02fe9ad' width='600'>
<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/2400f4a2-5c6c-4426-b99f-149a3a33687c' width='600'>

Example of getting _lesson by id_, where you can see information about the lesson, as well as a list of keywords and lessons that are recommended to be completed/repeated before.

<img src='https://github.com/yaryna-bashchak/maths-course/assets/90560209/24695d81-2e8a-43f6-8418-4db3fcb89956' width='700'>

### Tests
If you want to **run tests** (you also can see that all tests are passed in [_Github Actions_](https://github.com/yaryna-bashchak/maths-course/actions))

- go back to the root folder maths-course if you are not there
- run the following command

<code>$ dotnet test ./API.Tests</code>

Example:

![image](https://github.com/yaryna-bashchak/maths-course/assets/90560209/c05c51ec-8fde-42bd-9022-387070f7b4c3)

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
