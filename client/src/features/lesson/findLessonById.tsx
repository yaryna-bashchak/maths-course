import { Course } from "../../app/models/course";
import { Lesson } from "../../app/models/lesson";

export function findLessonById(course: Course, lessonId: number): Lesson | null {
    for (const section of course.sections) {
        for (const lesson of section.lessons) {
            if (lesson.id === lessonId) {
                return lesson;
            }
        }
    }
    return null;
}
