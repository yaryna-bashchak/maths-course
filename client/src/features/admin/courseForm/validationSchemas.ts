import * as yup from 'yup'

export const courseValidationSchema = yup.object({
  title: yup.string().required(),
  priceFull: yup.number().required().moreThan(0),
  priceMonthly: yup.number().required().moreThan(0),
  duration: yup.number().required().min(0),
  description: yup.string().required(),
  file: yup.mixed().when('pictureUrl', {
    is: (value: string) => !value,
    then: schema => schema.required('Please provide a file'),
    otherwise: schema => schema.notRequired()
  })
})

export const sectionValidationSchema = yup.object({
  title: yup.string().required(),
  description: yup.string()
})

const FILE_SIZE_LIMIT = 100000000 // 100MB
const SUPPORTED_FORMATS = [
  'video/mp4',
  'video/webm',
  'video/ogg',
  'video/x-ms-asf'
]

const videoSchema = yup
  .mixed()
  .test('fileSize', 'File too large', (value: any) => {
    const file = value as File
    return !file || (file && file.size <= FILE_SIZE_LIMIT)
  })
  .test('fileFormat', 'Unsupported Format', (value: any) => {
    const file = value as File
    if (!file) return true

    const fileType = file.type
    const extension = file.name.split('.').pop()?.toLowerCase()

    return fileType ? SUPPORTED_FORMATS.includes(fileType) : extension === 'asf'
  })
  .notRequired()

export const lessonValidationSchema = yup.object({
  title: yup.string().required(),
  importance: yup.number().required().min(0),
  description: yup.string(),
  theory: videoSchema,
  practice: videoSchema
})
