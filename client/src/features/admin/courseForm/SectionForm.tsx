import { useState, useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import { Section } from "../../../app/models/course";
import SectionHeader from "./SectionHeader";
import TableOfSectionLessons from "./TableOfSectionLessons";
import { Grid, TableCell, TableRow } from "@mui/material";
import AppTextInput from "../../../app/components/AppTextInput";
import { yupResolver } from '@hookform/resolvers/yup';
import { sectionValidationSchema } from "./validationSchemas";

interface Props {
    section?: Section;
}

export default function SectionForm({ section }: Props) {
    const [isEditing, setIsEditing] = useState(false);
    const { control, reset, handleSubmit } = useForm({
        resolver: yupResolver<any>(sectionValidationSchema),
        defaultValues: {
            title: section?.title || "",
            description: section?.description || ""
        }
    });

    useEffect(() => {
        reset({
            title: section?.title || "",
            description: section?.description || ""
        });
    }, [section, reset]);

    const toggleEdit = (resetForm = true) => {
        if (isEditing && resetForm) {
            reset({
                title: section?.title || "",
                description: section?.description || ""
            });
        }

        setIsEditing(!isEditing);
    };

    const handleSubmitData = (data: FieldValues) => {
        console.log("Title: ", data.title);
        console.log("Description: ", data.description);

        toggleEdit(false);
    }

    return (<>
        <SectionHeader section={section} handleEditClick={toggleEdit} handleSubmitData={handleSubmit(handleSubmitData)} isEditing={isEditing} />

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

        <TableOfSectionLessons section={section} />
    </>
    )
}