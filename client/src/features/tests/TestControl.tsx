import { FormControl, RadioGroup, FormControlLabel, Radio, Typography, Box, Button, FormHelperText } from "@mui/material";
import React, { useState } from "react";
import { Test, Option } from "../../app/models/test";
import { green, pink } from "@mui/material/colors";
import { useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { testSelectors } from "./testsSlice";
import { updateLessonAsync } from "../courses/coursesSlice";

interface Props {
    test: Test;
    completed: { [k: number]: number };
    activeStep: number;
    index: number;
    currentTestScore: number;
    setCurrentTestScore: React.Dispatch<React.SetStateAction<number>>;
    setActiveStep: React.Dispatch<React.SetStateAction<number>>;
    setCompleted: React.Dispatch<React.SetStateAction<{ [k: number]: number; }>>;
    setIsFinished: React.Dispatch<React.SetStateAction<boolean>>;
}

export default function TestControl({
    test,
    completed,
    activeStep,
    index,
    currentTestScore,
    setCurrentTestScore,
    setActiveStep,
    setCompleted,
    setIsFinished,
}: Props) {
    const dispatch = useAppDispatch();
    const { lessonId, courseId } = useParams<{ courseId: string, lessonId: string }>();
    const tests = useAppSelector(testSelectors.selectAll);
    const [checked, setChecked] = useState('');
    const [helperText, setHelperText] = useState(' ');
    const answerId = test.options.find(option => option.isAnswer === true)?.id;

    const totalSteps = (tests: Test[]): number => {
        return tests ? tests.length : 0;
    }

    const handleNext = () => {
        if (tests) {
            let newActiveStep = tests.findIndex((_, i) => !(i in completed) && i > activeStep);

            if (newActiveStep === -1) {
                newActiveStep = tests.findIndex((_, i) => !(i in completed));
            }

            setActiveStep((newActiveStep >= 0) ? newActiveStep : activeStep + 1);
        }
    };

    const completedSteps = () => {
        return Object.keys(completed).length;
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleComplete = (score: number) => {
        const newCompleted = { ...completed };
        setCurrentTestScore((prev) => prev + score);
        newCompleted[activeStep] = score;
        setCompleted(newCompleted);
    };

    const handleFinish = () => {
        setIsFinished(true);
        dispatch(updateLessonAsync({ id: parseInt(lessonId!), body: { testScore: 100 * currentTestScore / totalSteps(tests), courseId } }))
    }

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
        const total = totalSteps(tests);
        return completedCount === total;
    }

    const getRadioStyle = (option: Option) => {
        if (!(activeStep in completed)) return {};

        const isAnswer = option.id === answerId;
        const isChecked = option.id === Number(checked);

        if (isChecked) return isAnswer ? styles.greenRadio : styles.pinkRadio;
        if (isAnswer) return styles.greenRadio;
        return {};
    }

    const styles = {
        greenRadio: {
            color: green[800],
            '&.Mui-checked': {
                color: green[600],
            },
        },
        pinkRadio: {
            color: pink[800],
            '&.Mui-checked': {
                color: pink[600],
            },
        }
    };

    const getChecked = (option: Option) => {
        const isStepCompleted = Object.prototype.hasOwnProperty.call(completed, activeStep);
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
                                        control={<Radio sx={getRadioStyle(option)} />}
                                        label={option.text}
                                        disabled={activeStep in completed}
                                        checked={getChecked(option)}
                                        key={option.id}
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
                        disabled={activeStep === totalSteps(tests) - 1}
                        onClick={handleNext}
                        sx={{ mr: 1 }}>
                        Вперед
                    </Button>
                    {
                        activeStep !== totalSteps(tests) &&
                        (isAllCompleted() ? (
                            <Button variant="contained" onClick={handleFinish}>
                                Завершити тест
                            </Button>
                        ) : !Object.prototype.hasOwnProperty.call(completed, activeStep) ? (
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