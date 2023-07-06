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
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        id && agent.Course.details(parseInt(id))
            .then(course => {
                setCourse(course);
                setSections(course.sections);
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [id])

    const [openIndex, setOpenIndex] = useState(-1);

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };

    if (loading) return <h3>Loading...</h3>
    
    if (!course) return <h3>Course not found</h3>

    return (
        <>
            <Typography variant="h5">{course?.title}</Typography>
            <List className="list-border" sx={{p: "0px", m: "8px 0px"}}>
                {sections.map((section) =>
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