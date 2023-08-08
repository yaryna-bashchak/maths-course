import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { Course } from "../../app/models/course";
import agent from "../../app/api/agent";
import { RootState } from "../../app/store/configureStore";

const coursesAdapter = createEntityAdapter<Course>();

export const fetchCoursesAsync = createAsyncThunk<Course[]>(
    'courses/fetchCoursesAsync',
    async () => {
        try {
            return await agent.Course.list();
        } catch (error) {
            console.log(error);
        }
    }
)

export const coursesSlice = createSlice({
    name: 'courses',
    initialState: coursesAdapter.getInitialState({
        coursesLoaded: false,
        status: 'idle',
    }),
    reducers: {},
    extraReducers: (builder => {
        builder.addCase(fetchCoursesAsync.pending, (state) => {
            state.status = 'pendingFetchCourses';
        });
        builder.addCase(fetchCoursesAsync.fulfilled, (state, action) => {
            coursesAdapter.setAll(state, action.payload);
            state.status = 'idle';
            state.coursesLoaded = true;
        });
        builder.addCase(fetchCoursesAsync.rejected, (state) => {
            state.status = 'idle';
        })
    }),
})

export const courseSelectors = coursesAdapter.getSelectors((state: RootState) => state.courses);
