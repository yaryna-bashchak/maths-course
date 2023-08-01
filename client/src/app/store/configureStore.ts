import { createStore } from "redux";
import counterReducer from "../../features/home/counterReducer";

export function configureStore() {
    return createStore(counterReducer);
}