import { useState, useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import { Section } from "../../../app/models/course";
import SectionHeader from "./SectionHeader";
import TableOfSectionLessons from "./TableOfSectionLessons";
import { Box, Button, Grid, TableCell, TableRow } from "@mui/material";
import AppTextInput from "../../../app/components/AppTextInput";
import { yupResolver } from '@hookform/resolvers/yup';
import { sectionValidationSchema } from "./validationSchemas";

interface Props {
    section?: Section;
}

export default function SectionForm({ section }: Props) {
    const [isEditing, setIsEditing] = useState(false);
    const { control, reset } = useForm({
        resolver: yupResolver<any>(sectionValidationSchema),
        defaultValues: {
            title: "",
            description: ""
        }
    });

    useEffect(() => {
        if (section) {
            const sectionCopy = { ...section };

            if (sectionCopy.title === null) sectionCopy.title = "";
            if (sectionCopy.description === null) sectionCopy.description = "";

            reset(sectionCopy);
        }
    }, [section, reset]);

    const handleEditClick = () => {
        setIsEditing(!isEditing);
    };

    const handleSubmitData = () => {
        setIsEditing(!isEditing);
        console.log("submit");
    }

    return (<>
        <SectionHeader section={section} handleEditClick={handleEditClick} handleSubmitData={handleSubmitData} isEditing={isEditing} />

        {isEditing && (
            <TableRow>
                <TableCell style={{}} colSpan={3}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={4}>
                            <AppTextInput
                                control={control}
                                name='title'
                                label="Назва секції"
                                type="text"
                            />
                        </Grid>
                        <Grid item xs={12} sm={8}>
                            <AppTextInput
                                control={control}
                                name='description'
                                label="Опис"
                                multiline={true}
                                rows={2}
                                type="text"
                            />
                        </Grid>
                    </Grid>
                </TableCell >
            </TableRow>
        )}

        <TableOfSectionLessons lessons={section?.lessons} />
    </>
    )
}