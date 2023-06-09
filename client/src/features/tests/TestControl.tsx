import { FormControl, RadioGroup, FormControlLabel, Radio, Typography, Box, Button, FormHelperText } from "@mui/material";
import React, { useState } from "react";
import { Test, Option } from "../../app/models/test";
import { green, pink } from "@mui/material/colors";

interface Props {
    test: Test;
    handleNext: () => void;
    handleBack: () => void;
    handleComplete: (score: number) => void;
    handleFinish: () => void;
    handleReset: () => void;
    testScore: number;
    totalSteps: () => number;
    completedSteps: () => number;
    completed: { [k: number]: number };
    activeStep: number;
    index: number;
    isFinished: boolean;
}


export default function TestControl({
    test,
    handleNext,
    handleBack,
    handleComplete,
    handleFinish,
    testScore,
    totalSteps,
    completedSteps,
    completed,
    activeStep,
    index,
    isFinished,
}: Props) {
    const [checked, setChecked] = useState('');
    const [helperText, setHelperText] = useState(' ');
    const answerId = test.options.find(option => option.isAnswer === true)?.id;

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setChecked((event.target as HTMLInputElement).value);
        setHelperText(' ');
    };

    const submitAnswer = () => {
        if (checked === '') {
            setHelperText('Оберіть варіант перед тим, як відповісти')
        } else {
            handleComplete(Number(Number(checked) === answerId));
        }
    }

    const isAllCompleted = () => {
        const completedCount = completedSteps();
        const total = totalSteps();
        return completedCount === total;
    }

    const getStyles = (option: Option) => {
        const greenStyle = {
            color: green[800],
            '&.Mui-checked': {
                color: green[600],
            },
        };

        const pinkStyle = {
            color: pink[800],
            '&.Mui-checked': {
                color: pink[600],
            },
        };

        if (!(activeStep in completed)) {
            return {};
        }

        if (option.id === Number(checked)) {
            return option.id === answerId ? greenStyle : pinkStyle;
        } else if (option.id === answerId) {
            return greenStyle;
        } else {
            return {};
        }
    }

    const getChecked = (option: Option) => {
        const isStepCompleted = completed.hasOwnProperty(activeStep);
        const isAnswer = option.id === answerId;
        const isChecked = option.id === Number(checked);

        return (isStepCompleted && isAnswer) || isChecked;
    }

    return (
        <div hidden={index !== activeStep}>
            <>
                {test && (
                    <FormControl>
                        <Typography variant="h5">
                            {test.question}
                        </Typography>
                        <RadioGroup onChange={handleChange}>
                            {
                                test.options.map(option => (
                                    <FormControlLabel
                                        value={option.id}
                                        control={<Radio sx={getStyles(option)} />}
                                        label={option.text}
                                        disabled={activeStep in completed}
                                        checked={getChecked(option)}
                                    />
                                ))
                            }
                        </RadioGroup>
                        <FormHelperText sx={{ color: 'red' }}>{helperText}</FormHelperText>
                    </FormControl>)}
                <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
                    <Button
                        color="inherit"
                        disabled={activeStep === 0}
                        onClick={handleBack}
                        sx={{ mr: 1 }}
                    >
                        Назад
                    </Button>
                    <Box sx={{ flex: '1 1 auto' }} />
                    <Button
                        disabled={activeStep === totalSteps() - 1}
                        onClick={handleNext}
                        sx={{ mr: 1 }}>
                        Вперед
                    </Button>
                    {
                        activeStep !== totalSteps() &&
                        (isAllCompleted() ? (
                            <Button variant="contained" onClick={handleFinish}>
                                Завершити тест
                            </Button>
                        ) : !completed.hasOwnProperty(activeStep) ? (
                            <Button onClick={submitAnswer}>
                                Відповісти
                            </Button>
                        ) : (
                            <Button variant="contained" onClick={handleNext}>
                                Продовжити
                            </Button>
                        ))}
                </Box>
            </>
        </div>
    );
}