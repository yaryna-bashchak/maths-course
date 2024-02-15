import { useEffect } from "react";
import { courseSelectors, fetchCoursesAsync } from "../../features/courses/coursesSlice";
import { useAppSelector, useAppDispatch } from "../store/configureStore";

export default function useCourses() {
    const courses = useAppSelector(courseSelectors.selectAll);
    const { coursesLoaded, status } = useAppSelector(state => state.courses);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!coursesLoaded) dispatch(fetchCoursesAsync())
    }, [coursesLoaded, dispatch])

    return {
        courses,
        coursesLoaded,
        status
    }
}