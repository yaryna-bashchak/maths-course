import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice
} from '@reduxjs/toolkit'
import { Course } from '../../app/models/course'
import agent from '../../app/api/agent'
import { RootState } from '../../app/store/configureStore'
import { Lesson } from '../../app/models/lesson'

const coursesAdapter = createEntityAdapter<Course>()

export const fetchCoursesAsync = createAsyncThunk<Course[]>(
  'courses/fetchCoursesAsync',
  async () => {
    try {
      return await agent.Course.list()
    } catch (error) {
      console.log(error)
    }
  }
)

export const fetchCourseAsync = createAsyncThunk<Course, number>(
  'courses/fetchCourseAsync',
  async courseId => {
    try {
      return await agent.Course.details(courseId)
    } catch (error) {
      console.log(error)
    }
  }
)

export const updateLessonAsync = createAsyncThunk<
  Lesson,
  { id: number; body: {} }
>('courses/updateLessonAsync', async ({ id, body }, thunkAPI) => {
  try {
    return await agent.Lesson.update(id, body)
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data })
  }
})

export const coursesSlice = createSlice({
  name: 'courses',
  initialState: coursesAdapter.getInitialState({
    coursesLoaded: false,
    status: 'idle'
  }),
  reducers: {},
  extraReducers: builder => {
    builder.addCase(fetchCoursesAsync.pending, state => {
      state.status = 'pendingFetchCourses'
    })
    builder.addCase(fetchCoursesAsync.fulfilled, (state, action) => {
      const newCourses = action.payload.filter(
        course => !state.ids.includes(course.id)
      )

      coursesAdapter.addMany(state, newCourses)

      state.status = 'idle'
      state.coursesLoaded = true
    })
    builder.addCase(fetchCoursesAsync.rejected, state => {
      state.status = 'idle'
    })

    builder.addCase(fetchCourseAsync.pending, state => {
      state.status = 'pendingFetchCourse'
    })
    builder.addCase(fetchCourseAsync.fulfilled, (state, action) => {
      coursesAdapter.upsertOne(state, action.payload)
      state.status = 'idle'
    })
    builder.addCase(fetchCourseAsync.rejected, state => {
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
