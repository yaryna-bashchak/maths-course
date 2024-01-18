import { useEffect } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import CourseCard from "./CourseCard";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCoursesAsync } from "./coursesSlice";

export default function CourseCatalog() {
    const courses = useAppSelector(courseSelectors.selectAll);
    const { coursesLoaded, status } = useAppSelector(state => state.courses);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!coursesLoaded) dispatch(fetchCoursesAsync())
    }, [coursesLoaded, dispatch])

    if (status.includes('pending')) return <LoadingComponent />

    return (
        <>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '16px', justifyContent: 'center', textAlign: 'center' }}>
                    {courses.sort((a, b) => a.id - b.id).map((_course, index) =>
                        <CourseCard courseId={index + 1} key={index + 1}/>
                    )}
                </div>
        </>
    )
}