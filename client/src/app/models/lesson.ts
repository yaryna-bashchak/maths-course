export interface Lesson {
    id: number
    title: string
    description: string
    urlTheory: string
    urlPractice: string
    number: number
    importance: number
    isCompleted: boolean
    keywords?: Keyword[]
    previousLessons?: PreviousLesson[]
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
  