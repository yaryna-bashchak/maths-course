import { Box, Paper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { UserStatistic } from "../../app/models/test";
import agent from "../../app/api/agent";

interface Props {
    formatedTime: string;
    lessonId: number;
    testScore: number;
}

export default function Results({ formatedTime, lessonId, testScore }: Props) {
    const topPercent = 25;
    const [statistics, setStatistics] = useState<UserStatistic>();

    useEffect(() => {
        const fetchStatistics = async () => {
            try {
                const data = await agent.Test.getUserStatistic({ lessonId, topPercent });
                setStatistics(data);
            } catch (error) {
                console.error("Error fetching statistics:", error);
            }
        };

        fetchStatistics();
    }, [lessonId, topPercent]);

    const getMessage = () => {
        if (!statistics) return;

        if (testScore < 50)
            return 'Схоже, вам є над чим попрацювати. Спробуйте повторити тему для кращого засвоєння.';

        if (statistics.isUserInTopPercent)
            return `ЮХУ!.. Ви серед топ-${topPercent}% учасників з найкращими результами. Продовжуйте в тому ж дусі!`;

        if (testScore >= 80)
            return 'Ви отримали відмінний результат – 80% і більше! Чудова робота!';

        if (statistics.isScoreHigherThanAverage)
            return 'Ваш результат вище середнього серед інших учасників! Молодець!';

        return 'Супер! Це гарний показник, але ще є куди прямувати!'
    }

    return (<>
        <Paper elevation={3} sx={{ p: 3, m: '24px 0px 8px', borderRadius: 2 }}>
            <Box>
                <Typography variant="h6" sx={{ fontWeight: 'bold', color: 'primary.main' }}>
                    Вітаю! Твій результат: {testScore && testScore.toFixed(2)}%
                </Typography>
                <Typography variant="body1" sx={{ mt: 1, color: 'text.secondary' }}>
                    {getMessage()}
                </Typography>
                <Typography variant="caption" sx={{ mt: 1, display: 'block', color: 'gray' }}>
                    Час виконання: {formatedTime}
                </Typography>
            </Box>
        </Paper>
    </>);
}
