import { FormControlLabel, Radio } from "@mui/material";
import { green, pink } from "@mui/material/colors";
import { Option } from "../../app/models/test";

interface RadioButtonProps {
    option: Option;
    checked: string;
    isStepCompleted: boolean;
    answerId: number;
}

export default function RadioButtonComponent({
    option,
    checked,
    isStepCompleted,
    answerId,
}: RadioButtonProps) {
    const isAnswer = option.id === answerId;
    const isChecked = option.id === Number(checked);

    const getRadioStyle = () => {
        if (!isStepCompleted) return {};
        if (isChecked) return isAnswer ? styles.greenRadio : styles.pinkRadio;
        if (isAnswer) return styles.greenRadio;
        return {};
    };

    return (
        <FormControlLabel
            value={option.id}
            control={<Radio sx={getRadioStyle()} />}
            label={option.text}
            disabled={isStepCompleted}
            checked={(isStepCompleted && isAnswer) || isChecked}
        />
    );
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