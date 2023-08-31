import { createAsyncThunk, createSlice, isAnyOf } from '@reduxjs/toolkit'
import { User } from '../../app/models/user'
import { FieldValues } from 'react-hook-form'
import agent from '../../app/api/agent'
import { router } from '../../app/router/Routes'

interface AccountState {
  user: User | null
}

const initialState: AccountState = {
  user: null
}

export const singInUser = createAsyncThunk<User, FieldValues>(
  'account/singInUser',
  async (data, thunkAPI) => {
    try {
      const user = await agent.Account.login(data)
      localStorage.setItem('user', JSON.stringify(user))
      return user
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data })
    }
  }
)

export const fetchCurrentUser = createAsyncThunk<User>(
  'account/fetchCurrentUser',
  async (_, thunkAPI) => {
    try {
      const user = await agent.Account.currentUser()
      localStorage.setItem('user', JSON.stringify(user))
      return user
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data })
    }
  },
  {
    condition: () => {
      return Boolean(localStorage.getItem('user'))
    }
  }
)

export const accountSlice = createSlice({
  name: 'account',
  initialState,
  reducers: {
    signOut: state => {
      state.user = null
      localStorage.removeItem('user')
      router.navigate('/')
    }
  },
  extraReducers: builder => {
    builder.addMatcher(
      isAnyOf(singInUser.fulfilled, fetchCurrentUser.fulfilled),
      (state, action) => {
        state.user = action.payload
      }
    )
    builder.addMatcher(
      isAnyOf(singInUser.rejected, fetchCurrentUser.rejected),
      (state, action) => {
        console.log(action.payload)
      }
    )
  }
})

export const { signOut } = accountSlice.actions;
