import { useState, useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import { Section } from "../../../app/models/course";
import SectionHeader from "./SectionHeader";
import TableOfSectionLessons from "./TableOfSectionLessons";
import { Grid, TableCell, TableRow } from "@mui/material";
import AppTextInput from "../../../app/components/AppTextInput";
import { yupResolver } from '@hookform/resolvers/yup';
import { sectionValidationSchema } from "./validationSchemas";
import { Lesson } from "../../../app/models/lesson";
import { useAppDispatch } from "../../../app/store/configureStore";
import agent from "../../../app/api/agent";
import { removeSection, setSection } from "../../courses/coursesSlice";

interface Props {
    section?: Section;
    handleSelectLesson?: (lesson: Lesson | undefined) => void;
    courseId: number;
    numberOfNewSection?: number;
}

interface ActionLoadingState {
    submit?: boolean;
    delete?: boolean;
}

export interface LoadingState {
    sections: { [key: number]: ActionLoadingState };
    lessons: { [key: number]: ActionLoadingState };
}

type RemoveDispatch = (args: { id: number, courseId: number }) => { type: string; payload: number; }

export default function SectionForm({ section, handleSelectLesson, courseId, numberOfNewSection: number }: Props) {
    const [isEditing, setIsEditing] = useState(false);
    const dispatch = useAppDispatch();
    const [loadingState, setLoadingState] = useState<LoadingState>({ sections: {}, lessons: {} });
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

    const toggleEdit = () => {
        if (isEditing) {
            reset({
                title: section?.title || "",
                description: section?.description || ""
            });
        }

        setIsEditing(!isEditing);
    };

    const setLoading = (id: number, type: 'sections' | 'lessons', action: 'submit' | 'delete', isLoading: boolean) => {
        setLoadingState(prevState => ({
            ...prevState,
            [type]: {
                ...prevState[type],
                [id]: { ...prevState[type][id], [action]: isLoading }
            }
        }));
    };

    const handleDelete = async (id: number, type: 'sections' | 'lessons', deleteFunction: (id: number) => Promise<any>, removeDispatch: RemoveDispatch) => {
        setLoading(id, type, 'delete', true);

        try {
            await deleteFunction(id);
            dispatch(removeDispatch({ id, courseId }));
        } catch (error) {
            console.log(error);
        } finally {
            setLoading(id, type, 'delete', false);
        }
    };

    const handleSubmitSection = async (data: FieldValues) => {
        const sectionId = section ? section.id : -1;
        setLoading(sectionId, 'sections', 'submit', true);

        try {
            let response: Section;
            if (section) {
                const updatedSection = { ...section, ...data };
                response = await agent.Section.update(section.id, updatedSection);
            } else {
                const newSection = { ...data, courseId, number };
                response = await agent.Section.create(newSection);
            }

            dispatch(setSection(response));
            toggleEdit();
        } catch (error) {
            console.log(error);
        } finally {
            setLoading(sectionId, 'sections', 'submit', false);
        }
    }

    const handleDeleteSection = (id: number) => {
        handleDelete(id, 'sections', agent.Section.delete, removeSection);
    };

    // const handleDeleteLesson = (id: number) => {
    //     handleDelete(id, 'lessons', agent.Lesson.delete, removeLesson);
    // };

    return (<>
        <SectionHeader
            section={section}
            handleEditClick={toggleEdit}
            handleSubmitData={handleSubmit(handleSubmitSection)}
            handleDeleteData={handleDeleteSection}
            isEditing={isEditing}
            loadingState={loadingState}
        />

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

        <TableOfSectionLessons section={section} handleSelectLesson={handleSelectLesson} />
    </>
    )
}