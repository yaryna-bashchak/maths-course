import { List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Course, Section } from "../../app/models/course";
import { useParams } from "react-router-dom";
import axios from "axios";

export default function CourseDetails() {
    const [course, setCourse] = useState<Course>();
    const [sections, setSections] = useState<Section[]>([]);
    const [loading, setLoading] = useState(true);
    const {id} = useParams<{id: string}>();

    useEffect(() => {
        axios.get(`http://localhost:5000/api/courses/${id}`)
            .then(response => {
                setCourse(response.data);
                setSections(response.data.sections)
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