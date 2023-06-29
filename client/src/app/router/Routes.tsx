import { createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import CourseDetails from "../../features/lessonPanel/LessonList";
import LessonDetails from "../../features/lessonPanel/LessonDetails";
import AboutPage from "../../features/about/AboutPage";
import ContactPage from "../../features/contact/ContactPage";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'course/:id', element: <CourseDetails /> },
            { path: 'lesson/:id', element: <LessonDetails /> },
            { path: 'about', element: <AboutPage /> },
            { path: 'contact', element: <ContactPage /> },
        ]
    }
])