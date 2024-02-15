import { createAsyncThunk, createSlice, isAnyOf } from '@reduxjs/toolkit'
import { User } from '../../app/models/user'
import { FieldValues } from 'react-hook-form'
import agent from '../../app/api/agent'
import { router } from '../../app/router/Routes'
import { toast } from 'react-toastify'

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
    thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user')!)))
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
    },
    setUser: (state, action) => {
      const claims = JSON.parse(atob(action.payload.token.split('.')[1]))
      const roles =
        claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      state.user = {
        ...action.payload,
        roles: typeof roles === 'string' ? [roles] : roles
      }
    }
  },
  extraReducers: builder => {
    builder.addCase(fetchCurrentUser.rejected, state => {
      state.user = null
      localStorage.removeItem('user')
      toast.error('Сеанс завершився - будь ласка, увійдіть в акаунт ще раз')
      router.navigate('/')
    })
    builder.addMatcher(
      isAnyOf(singInUser.fulfilled, fetchCurrentUser.fulfilled),
      (state, action) => {
        const claims = JSON.parse(atob(action.payload.token.split('.')[1]))
        const roles =
          claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
        state.user = {
          ...action.payload,
          roles: typeof roles === 'string' ? [roles] : roles
        }
      }
    )
    builder.addMatcher(isAnyOf(singInUser.rejected), (_state, action) => {
      console.log(action.payload)
    })
  }
})

export const { signOut, setUser } = accountSlice.actions
