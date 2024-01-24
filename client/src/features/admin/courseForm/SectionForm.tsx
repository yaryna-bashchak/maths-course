import { Table, TableHead, TableRow, TableCell, TableBody, Button, useMediaQuery, useTheme } from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";
import { Section } from "../../../app/models/course";
import LessonLine from "./LessonLine";

interface Props {
    section?: Section;
}

export default function SectionForm({ section }: Props) {
    const theme = useTheme();
    const isMobileOrTablet = useMediaQuery(theme.breakpoints.down('md'));

    const sectionCellStyle = {
        fontSize: '1.1rem',
    };

    return (<>
        <TableRow
            sx={{
                backgroundColor: '#d0e3f7',
                // '&:last-child td, &:last-child th': { border: 0 }
            }}
        >
            <TableCell component="th" scope="row" sx={{
                ...sectionCellStyle,
                width: '40px'
            }}>
                ID={section?.id}
            </TableCell>
            <TableCell align="left" sx={{ ...sectionCellStyle, fontWeight: 'bold' }}>{section?.title}</TableCell>
            <TableCell align="right" colSpan={2}>
                <Button startIcon={<Edit />} />
                {/* <Button onClick={() => handleSelectCourse(section)} startIcon={<Edit />} /> */}
                <Button startIcon={<Delete />} color='error' disabled />
            </TableCell>
        </TableRow>
        <TableRow>
            <TableCell style={{ padding: '0 16px 0 40px' }} colSpan={3}>
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
                        {section?.lessons.map((lesson) => (
                            <LessonLine key={lesson.id} lesson={lesson} />
                        ))}
                    </TableBody>
                </Table>
            </TableCell>
        </TableRow>
    </>
    )
}