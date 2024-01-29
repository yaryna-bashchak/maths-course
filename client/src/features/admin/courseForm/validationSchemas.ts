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
    description: yup.string(),
})
