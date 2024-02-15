import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice
} from '@reduxjs/toolkit'
import { Course, LessonParams } from '../../app/models/course'
import agent from '../../app/api/agent'
import { RootState } from '../../app/store/configureStore'
import { Lesson } from '../../app/models/lesson'

interface CoursesState {
  coursesLoaded: boolean
  status: string
  individualCourseStatus: {
    [courseId: number]: {
      courseLoaded: boolean
      lessonParams: LessonParams
    }
  }
}

const coursesAdapter = createEntityAdapter<Course>()

function getAxiosParams (lessonParams: LessonParams) {
  const params = new URLSearchParams()
  if (lessonParams.maxImportance < 2)
    params.append('maxImportance', lessonParams.maxImportance.toString())
  if (lessonParams.onlyUncompleted)
    params.append('onlyUncompleted', lessonParams.onlyUncompleted.toString())
  if (lessonParams.searchTerm)
    params.append('searchTerm', lessonParams.searchTerm)
  return params
}

export const fetchCoursesAsync = createAsyncThunk<Course[]>(
  'courses/fetchCoursesAsync',
  async (_, thunkAPI) => {
    try {
      return await agent.Course.list()
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data })
    }
  }
)

export const fetchCourseAsync = createAsyncThunk<
  Course,
  number,
  { state: RootState }
>('courses/fetchCourseAsync', async (courseId, thunkAPI) => {
  const params = getAxiosParams(
    thunkAPI.getState().courses.individualCourseStatus[courseId].lessonParams
  )
  try {
    return await agent.Course.details(courseId, params)
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data })
  }
})

export const updateLessonAsync = createAsyncThunk<
  Lesson,
  { id: number; body: object }
>('courses/updateLessonAsync', async ({ id, body }, thunkAPI) => {
  try {
    return await agent.Lesson.updateCompletion(id, body)
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data })
  }
})

export function initLessonParams (): LessonParams {
  return {
    maxImportance: 2,
    onlyUncompleted: false
  }
}

const initializeCourseStatusLogic = (state: any, courseId: number) => {
  if (!state.individualCourseStatus[courseId]) {
    state.individualCourseStatus[courseId] = {
      courseLoaded: false,
      lessonParams: initLessonParams()
    }
  }
}

export const coursesSlice = createSlice({
  name: 'courses',
  initialState: coursesAdapter.getInitialState<CoursesState>({
    coursesLoaded: false,
    status: 'idle',
    individualCourseStatus: {}
  }),
  reducers: {
    setLessonParams: (state, action) => {
      //state.status = 'pendingFetchCourse'
      const { courseId } = action.payload
      state.individualCourseStatus[courseId] = {
        lessonParams: {
          ...state.individualCourseStatus[courseId].lessonParams,
          ...action.payload
        },
        courseLoaded: false
      }
      //state.individualCourseStatus[courseId].status = 'reloading'
    },
    initializeCourseStatus: (state, action) => {
      const { courseId } = action.payload
      initializeCourseStatusLogic(state, courseId)
      //state.status = 'pendingFetchCourse'
    },
    clearCourses: state => {
      state.entities = {}
      state.ids = []
      state.coursesLoaded = false
      state.individualCourseStatus = {}
    },
    setCourse: (state, action) => {
      const course = action.payload

      initializeCourseStatusLogic(state, course.id)

      coursesAdapter.upsertOne(state, course)
      state.individualCourseStatus[course.id].courseLoaded = true
    },
    removeCourse: (state, action) => {
      coursesAdapter.removeOne(state, action.payload)
    },
    setSection: (state, action) => {
      const section = action.payload
      const course = state.entities[section.courseId]

      if (course) {
        const existingSectionIndex = course.sections.findIndex(
          s => s.id === section.id
        )

        if (existingSectionIndex !== -1) {
          course.sections[existingSectionIndex] = section
        } else {
          course.sections.push(section)
        }
      }
    },
    removeSection: (state, action) => {
      const { id, courseId } = action.payload
      const course = state.entities[courseId]

      if (course) {
        course.sections = course.sections.filter(section => section.id !== id)
      }
    },
    setLesson: (state, action) => {
      const { lesson, sectionId } = action.payload

      for (const course of Object.values(state.entities)) {
        if (course?.sections) {
          for (const section of course.sections) {
            const lessonIndex = section.lessons.findIndex(
              l => l.id === lesson.id
            )

            if (lessonIndex !== -1) {
              section.lessons[lessonIndex] = lesson
            } else if (section.id === sectionId) {
              section.lessons.push(lesson)
            }
          }
        }
      }
    },
    removeLesson: (state, action) => {
      const { id, sectionId } = action.payload

      const course = Object.values(state.entities).find(course =>
        course?.sections.some(section => section.id === sectionId)
      )

      if (course) {
        const section = course.sections.find(
          section => section.id === sectionId
        )

        if (section) {
          section.lessons = section.lessons.filter(lesson => lesson.id !== id)
        }
      }
    }
  },
  extraReducers: builder => {
    builder.addCase(fetchCoursesAsync.pending, state => {
      state.status = 'pendingFetchCourses'
    })
    builder.addCase(fetchCoursesAsync.fulfilled, (state, action) => {
      //if (action.payload) {
      const newCourses = action.payload.filter(
        course => !state.ids.includes(course.id)
      )

      coursesAdapter.addMany(state, newCourses)
      state.coursesLoaded = true
      //}
      state.status = 'idle'
    })
    builder.addCase(fetchCoursesAsync.rejected, (state, action) => {
      console.log(action.payload)
      state.status = 'idle'
    })

    builder.addCase(fetchCourseAsync.pending, state => {
      state.status = 'pendingFetchCourse'
    })
    builder.addCase(fetchCourseAsync.fulfilled, (state, action) => {
      //if (action.payload) {
      coursesAdapter.upsertOne(state, action.payload)
      //}
      state.individualCourseStatus[action.payload.id].courseLoaded = true
      state.status = 'idle'
    })
    builder.addCase(fetchCourseAsync.rejected, (state, action) => {
      console.log(action.payload)
      state.status = 'idle'
    })

    builder.addCase(updateLessonAsync.pending, state => {
      state.status = 'pendingUpdateLesson'
    })
    builder.addCase(updateLessonAsync.fulfilled, (state, action) => {
      for (const course of Object.values(state.entities)) {
        if (course?.sections) {
          const section = course.sections.find(section =>
            section.lessons.some(lesson => lesson.id === action.payload.id)
          )

          if (section) {
            const lessonIndex = section.lessons.findIndex(
              lesson => lesson.id === action.payload.id
            )

            if (lessonIndex !== -1) {
              section.lessons[lessonIndex] = action.payload
            }
          }
        }
      }
      state.status = 'idle'
    })

    builder.addCase(updateLessonAsync.rejected, (state, action) => {
      state.status = 'idle'
      console.log(action.payload)
    })
  }
})

export const courseSelectors = coursesAdapter.getSelectors(
  (state: RootState) => state.courses
)

export const {
  setLessonParams,
  initializeCourseStatus,
  clearCourses,
  setCourse,
  removeCourse,
  setSection,
  removeSection,
  setLesson,
  removeLesson
} = coursesSlice.actions
