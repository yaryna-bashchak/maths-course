import { useMediaQuery, TableRow, TableCell, Table, TableHead, TableBody, useTheme } from "@mui/material";
import LessonLine from "./LessonLine";
import { Lesson } from "../../../app/models/lesson";

interface Props {
    lessons?: Lesson[];
}

export default function TableOfSectionLessons({ lessons }: Props) {
    const theme = useTheme();
    const isMobileOrTablet = useMediaQuery(theme.breakpoints.down('md'));

    return (<>
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
                        {lessons?.map((lesson) => (
                            <LessonLine key={lesson.id} lesson={lesson} />
                        ))}
                    </TableBody>
                </Table>
            </TableCell>
        </TableRow>
    </>
    )
}