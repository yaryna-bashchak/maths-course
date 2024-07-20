import { Elements } from "@stripe/react-stripe-js";
import CheckoutPage from "./CheckoutPage";
import { loadStripe } from "@stripe/stripe-js";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { useEffect } from "react";
import { fetchPayments } from "./paymentSlice";

const stripePromise = loadStripe('pk_test_51PdqcnLIgQOEBkZAnCWSvJxIw0oIZqgGnHQAA8dbAXNE0tVAfHCAsKljw4qm6V0t0mrBuMBrWezZjdH8s8MGlrdP00o0r1iWM1');

export default function CheckoutWrapper() {
    const dispatch = useAppDispatch();
    const { payments } = useAppSelector(state => state.payments);

    useEffect(() => {
        if (payments.length === 0) dispatch(fetchPayments());
    }, [dispatch, payments.length]);

    return (
        <Elements stripe={stripePromise}>
            <CheckoutPage />
        </Elements>
    )
}