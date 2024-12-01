import { Button } from "@mui/material";
import { Option } from "../../app/models/test";

interface ButtonCardProps {
    option: Option;
    checked: string;
    handleChange: (id: string) => void;
    isStepCompleted: boolean;
    answerId: number;
}

export default function ButtonCardComponent({
    option,
    checked,
    handleChange,
    isStepCompleted,
    answerId,
}: ButtonCardProps) {
    const isAnswer = option.id === answerId;
    const isChecked = option.id === Number(checked);

    const handleButtonClick = () => {
        if (!isStepCompleted) {
            handleChange(option.id.toString());
        }
    };

    const getBackgroundColor = () => {
        if (isStepCompleted) {
            if (isChecked) {
                return isAnswer ? "green" : "#e03d73";
            }
            return isAnswer ? "green" : "#e0e0e0";
        }
        return isChecked ? "#0346a3" : "#2196F3";
    };

    const getHoverBackgroundColor = () => {
        if (!isStepCompleted && isChecked) {
            return "#0346a3";
        }
        return "#2196F3";
    };

    return (
        <Button
            variant="contained"
            onClick={handleButtonClick}
            disabled={isStepCompleted}
            sx={{
                width: {
                    xs: "100%",
                    sm: "calc(50% - 20px)",
                },
                minWidth: "150px",
                height: "auto",
                minHeight: "80px",
                lineHeight: "1.25",
                backgroundColor: getBackgroundColor(),
                color: "white",
                fontSize: "20px",
                borderRadius: "8px",
                boxShadow: "0 4px 6px rgba(0, 0, 0, 0.1)",
                transition: "transform 0.1s ease-in-out",
                textTransform: "none",
                "&:hover": {
                    transform: !isStepCompleted ? "scale(1.05)" : "none",
                    backgroundColor: getHoverBackgroundColor(),
                },
                "&:active": {
                    transform: "scale(0.95)",
                },
                "&.Mui-disabled": {
                    backgroundColor: getBackgroundColor(),
                    color: isChecked || isAnswer ? "white" : "#a6a6ab",
                },
            }}
        >
            {option.text}
        </Button>
    );
}
