import { Box, Button, List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCourseAsync } from "../courses/coursesSlice";

export default function CourseDetails() {
    const dispatch = useAppDispatch();
    const { courseId } = useParams<{ courseId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const {status} = useAppSelector(state => state.courses);

    useEffect(() => {
        if(!course || course?.sections.length === 0) dispatch(fetchCourseAsync(parseInt(courseId!)));
    }, [course, courseId, dispatch])

    const [openIndex, setOpenIndex] = useState(-1);

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };

    if (status.includes('pending')) return <LoadingComponent />

    if (!course) return <NotFound />

    return (
        <>
            <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course`}>Назад до курсів</Button>
            </Box>
            <Typography variant="h5">{course?.title}</Typography>
            <List className="list-border" sx={{ p: "0px", m: "8px 0px" }}>
                {course.sections.map((section) =>
                    <SectionItem
                        key={section.id}
                        section={section}
                        courseId={parseInt(courseId ?? "0")}
                        isOpen={openIndex === section.id}
                        onItemClick={handleItemClick}
                    />
                )}
            </List>
        </>
    )
}