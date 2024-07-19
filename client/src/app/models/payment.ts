export interface Payment {
  id: number
  userId: string
  amount: number
  paymentDate: string
  paymentStatus: number
  paymentIntentId: string
  clientSecret: string
  purchaseType: string
  purchaseId: number
}
