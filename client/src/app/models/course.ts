import { Lesson, LessonPreview } from "./lesson"

export interface Course {
  id: number
  title: string
  description: string
  duration: number
  priceFull: number
  priceMonthly: number
  sections: Section[]
}

export interface Section {
  id: number
  number: number
  title: string
  description: string
  isAvailable: boolean
  lessons: Lesson[]
}

export interface CoursePreview {
  id: number
  title: string
  description: string
  duration: number
  priceFull: number
  priceMonthly: number
  sections: SectionPreview[]
}

export interface SectionPreview {
  id: number
  number: number
  title: string
  description: string
  isAvailable: number
  lessons: LessonPreview[]
}