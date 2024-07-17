import { Course } from "../../app/models/course";

type Stage = "notBought" | "boughtInPart" | "boughtInFull";

export default function stageOfCourse(course: Course | undefined): Stage {
    if (course?.sections.every(value => value.isAvailable === true)) {
        return "boughtInFull";
    } else if (course?.sections.some(value => value.isAvailable === true)) {
        return "boughtInPart";
    } else {
        return "notBought";
    }
}