import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import { useParams } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../app/store/configureStore';
import { courseSelectors, updateLessonAsync } from '../courses/coursesSlice';
import { findLessonById } from './LessonDetails';


export default function Videos() {
    const dispatch = useAppDispatch();
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const lesson = course ? findLessonById(course, parseInt(lessonId!)) : null;
    
    const handleContextMenu = (event: React.MouseEvent<HTMLVideoElement>) => {
        event.preventDefault();
    }

    return (
        <>
            <div className="video-wrapper">
                <video src="\videos\🍒 УРОК 7. ТЕОРІЯ _ буквенні вирази, ОДЗ, властивості дробів.mp4" controls controlsList="nodownload" onContextMenu={handleContextMenu}></video>
                <FormControlLabel
                    label="Theory"
                    control={
                        <Checkbox checked={lesson?.isTheoryCompleted} onChange={(event: React.ChangeEvent<HTMLInputElement>) =>
                            dispatch(updateLessonAsync({ id: parseInt(lessonId!), body: { isTheoryCompleted: Number(event.target.checked), courseId } }
                            ))
                        }
                        />
                    }
                />
            </div>
            <div className="video-wrapper">
                <video src="\videos\🍒УРОК 7. ПРАКТИКА.mp4" controls controlsList="nodownload" onContextMenu={handleContextMenu}></video>
                <FormControlLabel
                    label="Practice"
                    control={<Checkbox checked={lesson?.isPracticeCompleted} onChange={(event: React.ChangeEvent<HTMLInputElement>) =>
                        dispatch(updateLessonAsync({ id: parseInt(lessonId!), body: { isPracticeCompleted: Number(event.target.checked), courseId } }
                        ))
                    }
                    />}
                />
                {/* <iframe title="1" src="https://drive.google.com/file/d/1bmXiKjOnpqXDwXgWpqnnEBCXrBrofe2D/preview" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowFullScreen></iframe> */}
            </div>
        </>
    )
}