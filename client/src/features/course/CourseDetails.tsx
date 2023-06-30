import { List, Typography } from "@mui/material";
import SectionItem from "./SectionItem";
import { useEffect, useState } from "react";
import { Course, Section } from "../../app/models/course";

export default function CourseDetails() {
    const [course, setCourse] = useState<Course>();
    const [sections, setSections] = useState<Section[]>([]);

    useEffect(() => {
        fetch('http://localhost:5000/api/courses/1')
            .then(response => response.json())
            .then(data => {
                setCourse(data);
                setSections(data.sections)
            })
    }, [])

    const [openIndex, setOpenIndex] = useState(-1);

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };

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