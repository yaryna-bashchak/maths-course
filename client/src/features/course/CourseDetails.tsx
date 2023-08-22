import { Box, Button, List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCourseAsync, resetLessonParams } from "../courses/coursesSlice";
import Filters from "./Filters";
import SectionSkeleton from "./SectionSkeleton";

export default function CourseDetails() {
    const dispatch = useAppDispatch();
    const { courseId } = useParams<{ courseId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const { status, individualCourseLoaded, lessonParams } = useAppSelector(state => state.courses);

    useEffect(() => {
        if (!individualCourseLoaded[parseInt(courseId!)] || course?.sections.length === 0) dispatch(fetchCourseAsync(parseInt(courseId!)));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [individualCourseLoaded])

    useEffect(() => {
        if (course) {
            const index = firstUncompletedSection();
            setOpenIndex(index);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [course]);

    useEffect(() => {
        if (lessonParams.courseId !== parseInt(courseId!))
            dispatch(resetLessonParams({ courseId: parseInt(courseId!) }));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const firstUncompletedSection = () => {
        if (course) {
            for (let i = 0; i < course.sections.length; i++) {
                if (!course.sections[i].lessons.every(l =>
                    l.isPracticeCompleted && l.isTheoryCompleted && l.testScore >= 0
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


    if (status.includes('pending') && course?.sections.length === 0) return <LoadingComponent />;
    if (!course) return <NotFound />;

    return (
        <>
            <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course`}>Назад до курсів</Button>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'start', gap: '10px' }}>
                <Typography variant="h5">{course?.title}</Typography>
                <Filters />
            </Box>
            <List className="list-border" sx={{ p: "0px", m: "8px 0px" }}>
                {course.sections.map((section) =>
                    <SectionItem
                        key={section.id}
                        section={section}
                        isOpen={openIndex === section.id}
                        onItemClick={handleItemClick}
                    />
                )}
            </List>
        </>
    )
}