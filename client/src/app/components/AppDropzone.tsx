import { UploadFile } from '@mui/icons-material'
import { FormControl, Typography, FormHelperText } from '@mui/material'
import { Dispatch, SetStateAction, useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import { useController, UseControllerProps } from 'react-hook-form'

interface Props extends UseControllerProps {
    setPreviewUrl?: Dispatch<SetStateAction<string | undefined>>;
    currentPreviewUrl?: string | undefined;
}

export default function AppDropzone({ setPreviewUrl, currentPreviewUrl, ...otherProps }: Props) {
    const { fieldState, field } = useController({ ...otherProps, defaultValue: null })

    const dzStyles = {
        display: 'flex',
        border: 'dashed 3px #eee',
        borderColor: '#aaa',
        borderRadius: '5px',
        paddingTop: '30px',
        alignItems: 'center',
        height: 200,
    }

    const dzActive = {
        borderColor: 'green'
    }

    const onDrop = useCallback((acceptedFiles: any) => {
        if (currentPreviewUrl) {
            URL.revokeObjectURL(currentPreviewUrl);
        }

        const previewUrl = URL.createObjectURL(acceptedFiles[0]);
        if (setPreviewUrl) setPreviewUrl(previewUrl);

        field.onChange(acceptedFiles[0]);
    }, [currentPreviewUrl, field, setPreviewUrl])

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <div {...getRootProps()} style={{ maxWidth: 400, flex: '1 1 400px' }}>
            <FormControl style={isDragActive ? { ...dzStyles, ...dzActive } : dzStyles} error={!!fieldState.error} >
                <input {...getInputProps()} />
                <UploadFile sx={{ fontSize: '100px' }} />
                <Typography variant='h4' sx={{ textAlign: 'center' }}>Перетягніть відео сюди</Typography>
                <FormHelperText>{fieldState.error?.message}</FormHelperText>
            </FormControl>
        </div>
    )
}