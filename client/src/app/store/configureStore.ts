import { configureStore } from '@reduxjs/toolkit'
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'
import { coursesSlice } from '../../features/courses/coursesSlice'
import { testsSlice } from '../../features/tests/testsSlice'
import { accountSlice } from '../../features/account/accountSlice'
import paymentSlice from '../../features/checkout/paymentSlice'

export const store = configureStore({
  reducer: {
    courses: coursesSlice.reducer,
    account: accountSlice.reducer,
    tests: testsSlice.reducer,
    payments: paymentSlice,
  }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export const useAppDispatch = () => useDispatch<AppDispatch>()
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector
