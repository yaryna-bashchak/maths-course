import { useMediaQuery, TableRow, TableCell, Table, TableHead, TableBody, useTheme } from "@mui/material";
import LessonLine from "./LessonLine";
import { Section } from "../../../app/models/course";

interface Props {
    section?: Section;
}

export default function TableOfSectionLessons({ section }: Props) {
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
                                    <LessonLine key={lesson.id} lesson={lesson} />
                                ))}
                                <LessonLine />
                            </TableBody>
                        </Table>
                    </TableCell>
                </TableRow>
            }
        </>
    )
}