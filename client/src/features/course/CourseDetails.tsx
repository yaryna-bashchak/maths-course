import { Box, Button, List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCourseAsync, initializeCourseStatus } from "../courses/coursesSlice";
import Filters from "./Filters";
import SectionSkeleton from "./SectionSkeleton";

export default function CourseDetails() {
    const dispatch = useAppDispatch();
    const { courseId } = useParams<{ courseId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const { status } = useAppSelector(state => state.courses);
    const courseStatus = useAppSelector(state => state.courses.individualCourseStatus[parseInt(courseId!)]);
    const { courseLoaded, lessonParams } = courseStatus || {};

    useEffect(() => {
        if (!lessonParams)
            dispatch(initializeCourseStatus({ courseId: parseInt(courseId!) }));
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [courseStatus]);

    useEffect(() => {
        if (courseLoaded === false) dispatch(fetchCourseAsync(parseInt(courseId!)));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [courseLoaded])

    useEffect(() => {
        if (course) {
            const index = firstUncompletedSection();
            setOpenIndex(index);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [course]);

    const firstUncompletedSection = () => {
        if (course) {
            for (let i = 0; i < course.sections.length; i++) {
                if (!course.sections[i].lessons.every(l =>
                    l.isPracticeCompleted && l.isTheoryCompleted && l.testScore
                ))
                    return course.sections[i].id;
            }
        }
        return -1;
    }

    const [openIndex, setOpenIndex] = useState(firstUncompletedSection());

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };

    if ((!course || course.sections.length === 0) && !courseLoaded) {
        if (!lessonParams || status.includes('pending')) return <LoadingComponent />;
        return <NotFound />;
    }

    return (
        <>
            <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course`}>Назад до курсів</Button>
            </Box>
            <Box sx={{ display: 'flex', flexWrap: 'wrap', justifyContent: 'start', alignItems: "center", gap: '10px' }}>
                <Typography variant="h5">{course?.title}</Typography>
                <Filters />
            </Box>
            <List className="list-border" sx={{ p: "0px", m: "8px 0px" }}>
                {course && course.sections.map((section) =>
                    section.lessons.length !== 0 ?
                        !courseLoaded ?
                            <SectionSkeleton
                                key={section.id}
                                section={section}
                                isOpen={openIndex === section.id}
                            /> :
                            <SectionItem
                                key={section.id}
                                section={section}
                                isOpen={openIndex === section.id}
                                onItemClick={handleItemClick}
                            /> : <></>
                )}
            </List>
        </>
    )
}