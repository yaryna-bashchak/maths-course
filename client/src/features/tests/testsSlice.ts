import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice
} from '@reduxjs/toolkit'
import { Test } from '../../app/models/test'
import agent from '../../app/api/agent'
import { RootState } from '../../app/store/configureStore'

const testsAdapter = createEntityAdapter<Test>()

export const fetchTestsAsync = createAsyncThunk<Test[], number>(
  'tests/fetchTestsAsync',
  async lessonId => {
    try {
      return await agent.Test.details(lessonId)
    } catch (error) {
      console.log(error)
    }
  }
)

export const testsSlice = createSlice({
  name: 'tests',
  initialState: testsAdapter.getInitialState({
    lessonId: 0,
    status: 'idle'
  }),
  reducers: {},
  extraReducers: builder => {
    builder.addCase(fetchTestsAsync.pending, state => {
      state.status = 'pendingFetchTests'
    })
    builder.addCase(fetchTestsAsync.fulfilled, (state, action) => {
      if (action.payload) {
        testsAdapter.setAll(state, action.payload)
        state.lessonId = action.meta.arg
      }
      state.status = 'idle'
    })
    builder.addCase(fetchTestsAsync.rejected, state => {
      state.status = 'idle'
    })
  }
})

export const testSelectors = testsAdapter.getSelectors(
  (state: RootState) => state.tests
)
