import { FormControl, RadioGroup, FormControlLabel, Radio, Typography, Box, Button } from "@mui/material";
import React, { useState } from "react";
import { Test } from "../../app/models/test";

interface Props {
    test: Test;
    handleNext: () => void;
    handleBack: () => void;
    handleComplete: (score: number) => () => void;
    handleFinish: () => void;
    handleReset: () => void;
    testScore: number;
    totalSteps: () => number;
    completedSteps: () => number;
    completed: { [k: number]: number };
    activeStep: number;
    isFinished: boolean;
}


export default function TestControl({
    test,
    handleNext,
    handleBack,
    handleComplete,
    handleFinish,
    handleReset,
    testScore,
    totalSteps,
    completedSteps,
    completed,
    activeStep,
    isFinished,
}: Props) {
    const [value, setValue] = useState('');

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValue((event.target as HTMLInputElement).value);
    };

    const checkAnswer = () => {
        const answer = test.options.find(option => option.isAnswer === true);
        const choosen = test.options.find(option => option.text === value);
        return Number(answer === choosen);
    }

    const isAllCompleted = () => {
        const completedCount = completedSteps();
        const total = totalSteps();
        return completedCount === total;
    }

    return (
        <div>
            {isFinished ? (
                <>
                    <Typography sx={{ mt: 2, mb: 1 }}>
                        Вітаю! Твій результат: {(100 * testScore / totalSteps()).toFixed(2)}%
                    </Typography>
                    <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
                        <Box sx={{ flex: '1 1 auto' }} />
                        <Button onClick={handleReset}>Перескласти</Button>
                    </Box>
                </>
            ) : (
                <>
                    {test && (
                        <FormControl>
                            <Typography variant="h5">
                                {test.question}
                            </Typography>
                            <RadioGroup onChange={handleChange}>
                                {
                                    test.options.map(option => (
                                        <FormControlLabel value={option.text} control={<Radio />} label={option.text} />
                                    ))
                                }
                            </RadioGroup>
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
                        <Button onClick={handleNext} sx={{ mr: 1 }}>
                            Вперед
                        </Button>
                        {
                            activeStep !== totalSteps() &&
                            (isAllCompleted() ? (
                                <Button onClick={handleFinish}>
                                    Завершити тест
                                </Button>
                            ) : completed[activeStep] >= 0 ? (
                                <Typography variant="caption" sx={{ display: 'inline-block' }}>
                                    Тест {activeStep + 1} вже виконаний
                                </Typography>
                            ) : (
                                <Button onClick={handleComplete(checkAnswer())}>
                                    Відповісти
                                </Button>
                            ))}
                    </Box>
                </>
            )}
        </div>
    );
}