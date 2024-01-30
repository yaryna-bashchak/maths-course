import { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { courseSelectors, initializeCourseStatus, fetchCourseAsync } from '../../features/courses/coursesSlice';
import { useAppDispatch, useAppSelector } from '../store/configureStore';

export default function useCourse(passedCourseId?: number) {
    const dispatch = useAppDispatch();
    const { courseId: paramCourseId } = useParams<{ courseId: string }>();
    const courseId = passedCourseId !== undefined ? passedCourseId : (paramCourseId ? parseInt(paramCourseId) : undefined);

    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const courseStatus = useAppSelector(state => state.courses.individualCourseStatus[courseId!]);
    const { status } = useAppSelector(state => state.courses);
    const { courseLoaded, lessonParams } = courseStatus || {};

    useEffect(() => {
        if (!lessonParams) {
            dispatch(initializeCourseStatus({ courseId: courseId }));
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [courseStatus]);

    useEffect(() => {
        if (!courseLoaded && courseId) {
            dispatch(fetchCourseAsync(courseId));
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [courseLoaded]);

    return {
        course: course,
        courseLoaded,
        lessonParams,
        status
    };
}
