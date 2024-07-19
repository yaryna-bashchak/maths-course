import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import { Payment } from '../../app/models/payment'
import agent from '../../app/api/agent'

interface PaymentState {
  payments: Payment[]
  loading: boolean
  error: string | null
}

const initialState: PaymentState = {
  payments: [],
  loading: false,
  error: null
}

export const fetchPayments = createAsyncThunk<Payment[]>(
  'payments/fetchPayments',
  async (_, thunkAPI) => {
    try {
      return await agent.Payments.getAll()
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data })
    }
  }
)

export const createOrUpdatePayment = createAsyncThunk<
  Payment,
  { body: object }
>('payments/createOrUpdatePayment', async ({ body }, thunkAPI) => {
  try {
    return await agent.Payments.createOrUpdatePaymentIntent(body)
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data })
  }
})

export const paymentSlice = createSlice({
  name: 'payment',
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchPayments.pending, state => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchPayments.fulfilled, (state, action) => {
        state.loading = false
        state.payments = action.payload
      })
      .addCase(fetchPayments.rejected, (state, action) => {
        state.loading = false
        state.error = action.payload as string
      })
      .addCase(createOrUpdatePayment.pending, state => {
        state.loading = true
        state.error = null
      })
      .addCase(createOrUpdatePayment.fulfilled, (state, action) => {
        state.loading = false
        const index = state.payments.findIndex(
          payment => payment.id === action.payload.id
        )
        if (index !== -1) {
          state.payments[index] = action.payload
        } else {
          state.payments.push(action.payload)
        }
      })
      .addCase(createOrUpdatePayment.rejected, (state, action) => {
        state.loading = false
        state.error = action.payload as string
      })
  }
})

export default paymentSlice.reducer
