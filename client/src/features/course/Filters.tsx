import { Button } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import StarPurple500RoundedIcon from '@mui/icons-material/StarPurple500Rounded';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import { grey, yellow } from '@mui/material/colors';
import { fetchCourseAsync, setLessonParams } from "../courses/coursesSlice";
import { useParams } from "react-router-dom";

export default function Filters() {
    const dispatch = useAppDispatch();
    const { lessonParams } = useAppSelector(state => state.courses);
    const { courseId } = useParams<{ courseId: string }>();


    const getBoolButtonStyle = (state: boolean) => {
        if (state) return styles.checked;
        return styles.unchecked;
    }

    const styles = {
        unchecked: {
            textTransform: 'none',
            fontSize: 11,
            '& .MuiButton-endIcon': {
                marginLeft: '2px',
                color: grey[400],
            },
        },
        checked: {
            textTransform: 'none',
            fontSize: 11,
            borderColor: 'transparent',
            color: 'rgb(0, 140, 0)',
            boxShadow: '0 0 0 0.1rem rgba(0,155,0,.5)',
            '&:hover': {
                borderColor: 'rgba(0, 155, 0,.5)',
                boxShadow: '0 0 0 0.1rem rgba(0, 155, 0, 0.5)',
            },
            '& .MuiButton-endIcon': {
                marginLeft: '2px',
                color: yellow[800],
            },
        }
    };

    return (
        <>
            <Button sx={getBoolButtonStyle(lessonParams.maxImportance === 0)} variant="outlined" size="small"
                endIcon={<StarPurple500RoundedIcon />}
                onClick={() => {
                    dispatch(setLessonParams({ maxImportance: Math.abs(lessonParams.maxImportance - 2) }));
                    dispatch(fetchCourseAsync(parseInt(courseId!)));
                }}>
                лише без зірочок
            </Button>
            <Button sx={getBoolButtonStyle(lessonParams.onlyUncompleted)} variant="outlined" size="small"
                endIcon={<AccessTimeIcon />}
                onClick={() => {
                    dispatch(setLessonParams({ onlyUncompleted: !lessonParams.onlyUncompleted }))
                    dispatch(fetchCourseAsync(parseInt(courseId!)));
                }}>
                лише не завершені
            </Button>
        </>
    )
}