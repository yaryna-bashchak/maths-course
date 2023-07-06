import axios, { AxiosError, AxiosResponse } from 'axios'
import { toast } from 'react-toastify'
import { router } from '../router/Routes'

const sleep = () => new Promise(resolve => setTimeout(resolve, 300));

axios.defaults.baseURL = 'http://localhost:5000/api/'

const responseBody = (response: AxiosResponse) => response.data

axios.interceptors.response.use(
    async response => {
        await sleep();
        return response;
    },
    (error: AxiosError) => {
        const { data, status } = error.response as AxiosResponse;
        switch (status) {
            case 400:
                if (data.errors) {
                    const modelStateErrors: string[] = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modelStateErrors.push(data.errors[key]);
                        }
                    }
                    throw modelStateErrors.flat();
                }
                toast.error(data.title);
                break;
            case 401:
                toast.error(data.title);
                break;
            case 500:
                router.navigate('/server-error', {state: {error: data}});
                break;
            default:
                break;
        }

    return Promise.reject(error.response)
    }
)

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody)
}

const Course = {
  list: (id: number) => requests.get(`courses`),
  details: (id: number) => requests.get(`courses/${id}`)
}

const Lesson = {
  // list: (id: number) => requests.get(`lessons`),
  details: (id: number) => requests.get(`lessons/${id}`),
  update: (id: number, body: {}) => requests.put(`lessons/${id}`, body)
}

const Test = {
  details: (lessonId: number) => requests.get(`tests/lessonId/${lessonId}`),
  update: (lessonId: number, body: {}) =>
    requests.put(`lessons/${lessonId}`, body)
}

const TestErrors = {
  get400Error: () => requests.get('buggy/bad-request'),
  get401Error: () => requests.get('buggy/unauthorised'),
  get404Error: () => requests.get('buggy/not-found'),
  get500Error: () => requests.get('buggy/server-error'),
  getValidationError: () => requests.get('buggy/validation-error')
}

const agent = {
  Course,
  Lesson,
  Test,
  TestErrors
}

export default agent
