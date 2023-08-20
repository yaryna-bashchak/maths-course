import { Box, Button, List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCourseAsync } from "../courses/coursesSlice";
import Filters from "./Filters";

export default function CourseDetails() {
    const dispatch = useAppDispatch();
    const { courseId } = useParams<{ courseId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const { status } = useAppSelector(state => state.courses);

    useEffect(() => {
        if (!course || course?.sections.length === 0) dispatch(fetchCourseAsync(parseInt(courseId!)));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [course])

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

    if (status.includes('pending')) return <LoadingComponent />;

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