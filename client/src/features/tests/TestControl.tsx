import { FormControl, /*RadioGroup,*/ Typography, Box, Button, FormHelperText } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Option, Test } from "../../app/models/test";
import { useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { testSelectors } from "./testsSlice";
import { updateLessonAsync } from "../courses/coursesSlice";
//import RadioButtonComponent from "./RadioButtonComponent";
import ButtonCardComponent from "./ButtonCardComponent";

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
    const [helperText, setHelperText] = useState('');
    const [shuffledOptions, setShuffledOptions] = useState(test.options);
    const answerId = test.options.find(option => option.isAnswer === true)?.id;
    
    useEffect(() => {
        setShuffledOptions(shuffleArray([...test.options]));
    }, [test.options]);

    const shuffleArray = (array: Option[]) => {
        for (let i = array.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [array[i], array[j]] = [array[j], array[i]];
        }
        return array;
    };

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

    // const handleChangeOld = (event: React.ChangeEvent<HTMLInputElement>) => {
    //     setChecked((event.target as HTMLInputElement).value);
    //     setHelperText('');
    // };

    const handleChange = (id: string) => {
        setChecked(id);
        setHelperText('');
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

    return (
        <div hidden={index !== activeStep}>
            <>
                {test && (
                    <FormControl sx={{ width: "100%", rowGap: "20px", marginTop: "20px" }}>
                        <Typography variant="h5" sx={{ paddingLeft: "8px", fontSize: "20px" }}>
                            {index + 1}. {test.question}
                        </Typography>
                        <Box
                            sx={{
                                display: "flex",
                                flexWrap: "wrap",
                                justifyContent: "center",
                                gap: "20px",
                            }}
                        >
                            {shuffledOptions.map((option) => {
                                const isStepCompleted = Object.prototype.hasOwnProperty.call(
                                    completed,
                                    activeStep
                                );
                                return (
                                    <ButtonCardComponent
                                        key={option.id}
                                        option={option}
                                        checked={checked}
                                        handleChange={handleChange}
                                        isStepCompleted={isStepCompleted}
                                        answerId={answerId!}
                                    />
                                );
                            })}
                        </Box>
                        {/* <RadioGroup onChange={handleChangeOld}>
                            {
                                shuffledOptions.map(option => {
                                    const isStepCompleted = Object.prototype.hasOwnProperty.call(completed, activeStep);

                                    return (
                                        <RadioButtonComponent
                                            key={option.id}
                                            option={option}
                                            checked={checked}
                                            isStepCompleted={isStepCompleted}
                                            answerId={answerId!}
                                        />
                                    )
                                })
                            }
                        </RadioGroup> */}
                        {helperText && (
                            <FormHelperText sx={{ color: 'red', marginLeft: '8px', fontSize: '1rem' }}>
                                {helperText}
                            </FormHelperText>
                        )}
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