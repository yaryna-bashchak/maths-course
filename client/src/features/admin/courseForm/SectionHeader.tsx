import { Edit, Close, Done, Delete } from "@mui/icons-material";
import { TableRow, TableCell, Button } from "@mui/material";
import { Section } from "../../../app/models/course";

interface Props {
    section?: Section;
    handleEditClick: () => void;
    handleSubmitData: () => void;
    isEditing: boolean;
}

export default function SectionHeader({ section, handleEditClick, handleSubmitData, isEditing }: Props) {
    const sectionCellStyle = {
        fontSize: '1.1rem',
    };

    return (
        <>
            <TableRow sx={{
                backgroundColor: '#d0e3f7',
                '&:last-child td, &:last-child th': { border: 0 }
            }}>
                <TableCell component="th" scope="row" sx={{ ...sectionCellStyle, width: '40px' }}>
                    ID={section?.id}
                </TableCell>

                <TableCell align="left" sx={{ ...sectionCellStyle, fontWeight: 'bold' }}>{section?.title}</TableCell>

                <TableCell align="right" colSpan={2}>
                    {isEditing && <>
                        <Button onClick={handleSubmitData} startIcon={<Done />} color='success' />
                        <Button onClick={handleEditClick} startIcon={<Close />} color='error' />
                    </>}
                    <Button onClick={handleEditClick} startIcon={<Edit />} disabled={isEditing} />
                    <Button startIcon={<Delete /> } color='error' disabled={isEditing} />
                </TableCell>
            </TableRow>
        </>
    )
}