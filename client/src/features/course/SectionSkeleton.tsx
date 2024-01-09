import { Box, Collapse, ListItemButton, Skeleton } from "@mui/material";
import { Section } from "../../app/models/course";

interface Props {
    section: Section;
    isOpen: boolean;
}

export default function SectionSkeleton({ section, isOpen }: Props) {
    return (
        <>
            <ListItemButton className='item-border'>
                <Skeleton animation="wave" variant="circular" height={30} width={30} style={{ margin: '10px 12px 10px 0px' }} />
                <Box sx={{ dispaly: 'flex', width: '100%' }}>
                    <Skeleton animation="wave" height={30} width={65} />
                    <Skeleton animation="wave" height={15} width={75} sx={{ opacity: 0.5 }} />
                </Box>
            </ListItemButton>
            <Collapse className="item-border" in={isOpen} timeout="auto" unmountOnExit sx={{ pl: "16px" }}>
                {section.lessons.map(() =>
                    <ListItemButton sx={{p: '0px 16px'}}>
                        <Skeleton animation="wave" variant="circular" height={15} width={15} style={{ margin: '10px 12px 10px 0px' }} />
                        <Skeleton animation="wave" height={20} width='30%' />
                    </ListItemButton>
                )}
            </Collapse>
        </>
    )
}