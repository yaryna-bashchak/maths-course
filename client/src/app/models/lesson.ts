export interface Lesson {
  id: number
  title: string
  description: string
  urlTheory: string
  urlPractice: string
  number: number
  importance: number
  testScore: number | null;
  isTheoryCompleted: boolean
  isPracticeCompleted: boolean
  keywords?: Keyword[]
  previousLessons?: PreviousLesson[]
}

export interface LessonPreview {
  id: number
  title: string
  description: string
}

export interface Keyword {
  id?: number
  word: string
}

export interface PreviousLesson {
  id: number
  title?: string
  isCompleted?: boolean
}
