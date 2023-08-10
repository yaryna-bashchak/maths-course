import { configureStore } from '@reduxjs/toolkit'
import { counterSlice } from '../../features/home/counterSlice'
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'
import { coursesSlice } from '../../features/courses/coursesSlice'
import { testsSlice } from '../../features/tests/testsSlice'

export const store = configureStore({
  reducer: {
    counter: counterSlice.reducer,
    courses: coursesSlice.reducer,
    tests: testsSlice.reducer
  }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export const useAppDispatch = () => useDispatch<AppDispatch>()
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector
