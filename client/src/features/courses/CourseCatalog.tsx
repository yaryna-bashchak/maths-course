import { useEffect } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import CourseCard from "./CourseCard";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCoursesAsync } from "./coursesSlice";
import { Grid } from "@mui/material";

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
            <Grid container spacing={4}>
                {courses.sort((a, b) => a.id - b.id).map((course) =>
                    <Grid item xs={4} key={course.id}>
                        <CourseCard course={course} />
                    </Grid>
                )}
            </Grid>
        </>
    )
}