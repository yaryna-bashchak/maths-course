import { Course } from "../../app/models/course";

export function isAvailable(course: Course | undefined, lessonId: string) {
    const section = course ? course.sections.find(s => s.lessons.some(l => l.id === parseInt(lessonId))) : null;
    return section?.isAvailable;
}
