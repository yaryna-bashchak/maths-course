import { UploadFile } from '@mui/icons-material'
import { FormControl, Typography, FormHelperText } from '@mui/material'
import { useCallback, useState } from 'react'
import { useDropzone } from 'react-dropzone'
import { useController, UseControllerProps } from 'react-hook-form'

interface Props extends UseControllerProps { }

export default function AppDropzone(props: Props) {
    const { fieldState, field } = useController({ ...props, defaultValue: null })
    const [prevUrl, setPrevUrl] = useState(null)

    const dzStyles = {
        display: 'flex',
        border: 'dashed 3px #eee',
        borderColor: '#aaa',
        borderRadius: '5px',
        paddingTop: '30px',
        alignItems: 'center',
        height: 200,
        width: 400
    }

    const dzActive = {
        borderColor: 'green'
    }

    const onDrop = useCallback((acceptedFiles: any) => {
        if (prevUrl) {
            URL.revokeObjectURL(prevUrl);
        }

        console.log(acceptedFiles[0])
        const fileWithPreview = Object.assign({}, acceptedFiles[0], {
            preview: URL.createObjectURL(acceptedFiles[0]),
            type: acceptedFiles[0].type,
            size: acceptedFiles[0].size,
        });

        setPrevUrl(fileWithPreview.preview)
        field.onChange(fileWithPreview);
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [field])

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <div {...getRootProps()}>
            <FormControl style={isDragActive ? { ...dzStyles, ...dzActive } : dzStyles} error={!!fieldState.error} >
                <input {...getInputProps()} />
                <UploadFile sx={{ fontSize: '100px' }} />
                <Typography variant='h4'>Перетягніть відео сюди</Typography>
                <FormHelperText>{fieldState.error?.message}</FormHelperText>
            </FormControl>
        </div>
    )
}