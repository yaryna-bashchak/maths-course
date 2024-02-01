import { useMediaQuery, TableRow, TableCell, Table, TableHead, TableBody, useTheme } from "@mui/material";
import LessonLine from "./LessonLine";
import { Section } from "../../../app/models/course";
import { Lesson } from "../../../app/models/lesson";

interface Props {
    section?: Section;
    handleSelectLesson?: (lesson: Lesson | undefined) => void;
    handleDeleteLesson: (id: number) => void;
}

export default function TableOfSectionLessons({ section, handleSelectLesson, handleDeleteLesson }: Props) {
    const theme = useTheme();
    const isMobileOrTablet = useMediaQuery(theme.breakpoints.down('md'));

    return (
        <>
            {section &&
                <TableRow>
                    <TableCell style={{ padding: '0 16px 0 40px' }} colSpan={4}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>№</TableCell>
                                    <TableCell align="left">Назва уроку</TableCell>
                                    {!isMobileOrTablet && <TableCell align="center">Важливість</TableCell>}
                                    <TableCell align="right"></TableCell>
                                </TableRow>
                            </TableHead>

                            <TableBody>
                                {section?.lessons?.map((lesson) => (
                                    <LessonLine key={lesson.id} lesson={lesson} handleSelectLesson={handleSelectLesson} handleDeleteLesson={handleDeleteLesson}/>
                                ))}
                                <LessonLine handleSelectLesson={handleSelectLesson} />
                            </TableBody>
                        </Table>
                    </TableCell>
                </TableRow>
            }
        </>
    )
}