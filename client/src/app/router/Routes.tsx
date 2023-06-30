import { createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import CourseDetails from "../../features/course/CourseDetails";
import LessonDetails from "../../features/lesson/LessonDetails";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'course/:id', element: <CourseDetails /> },
            { path: 'lesson/:id', element: <LessonDetails /> },
        ]
    }
])