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

export const fetchCourseAsync = createAsyncThunk<Course, number>(
    'courses/fetchCourseAsync',
    async (courseId) => {
        try {
            return await agent.Course.details(courseId);
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
        });

        builder.addCase(fetchCourseAsync.pending, (state) => {
            state.status = 'pendingFetchCourse';
        });
        builder.addCase(fetchCourseAsync.fulfilled, (state, action) => {
            coursesAdapter.upsertOne(state, action.payload);
            state.status = 'idle';
        });
        builder.addCase(fetchCourseAsync.rejected, (state) => {
            state.status = 'idle';
        });
    }),
})

export const courseSelectors = coursesAdapter.getSelectors((state: RootState) => state.courses);
