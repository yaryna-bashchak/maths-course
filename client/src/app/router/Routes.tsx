import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import CourseDetails from "../../features/course/CourseDetails";
import LessonDetails from "../../features/lesson/LessonDetails";
import TestsSpace from "../../features/tests/TestsSpace";
import NotFound from "../errors/NotFound";
import ServerError from "../errors/ServerError";
import CourseCatalog from "../../features/courses/CourseCatalog";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'course', element: <CourseCatalog /> },
            { path: 'course/:courseId', element: <CourseDetails /> },
            { path: 'course/:courseId/lesson/:lessonId', element: <LessonDetails /> },
            { path: 'course/:courseId/lesson/:lessonId/test', element: <TestsSpace /> },
            { path: 'server-error', element: <ServerError /> },
            { path: 'not-found', element: <NotFound /> },
            { path: '*', element: <Navigate replace to='not-found' /> },
        ]
    }
])