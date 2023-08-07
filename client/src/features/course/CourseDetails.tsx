import { List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Course, Section } from "../../app/models/course";
import { useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default function CourseDetails() {
    const [course, setCourse] = useState<Course>();
    const [sections, setSections] = useState<Section[]>([]);
    const [loading, setLoading] = useState(true);
    const { courseId } = useParams<{ courseId: string }>();

    useEffect(() => {
        courseId && agent.Course.details(parseInt(courseId))
            .then(course => {
                setCourse(course);
                setSections(course.sections);
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [courseId])

    const [openIndex, setOpenIndex] = useState(-1);

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };

    if (loading) return <LoadingComponent />

    if (!course) return <NotFound />

    return (
        <>
            <Typography variant="h5">{course?.title}</Typography>
            <List className="list-border" sx={{ p: "0px", m: "8px 0px" }}>
                {sections.map((section) =>
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