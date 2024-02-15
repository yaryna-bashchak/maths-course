import LoadingComponent from "../../app/layout/LoadingComponent";
import CourseCard from "./CourseCard";
import useCourses from "../../app/hooks/useCourses";

export default function CourseCatalog() {
    const { courses, status } = useCourses();

    if (status.includes('pending')) return <LoadingComponent />

    return (
        <>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '16px', justifyContent: 'center', textAlign: 'center' }}>
                    {courses.sort((a, b) => a.id - b.id).map((_course, index) =>
                        <CourseCard courseId={index + 1} key={index + 1}/>
                    )}
                </div>
        </>
    )
}