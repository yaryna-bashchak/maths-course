import { Card, CardMedia, CardContent, Typography, CardActions, Button, Box } from "@mui/material";
import { Link } from "react-router-dom";
import { useAppSelector } from "../../app/store/configureStore";
import { courseSelectors } from "./coursesSlice";

interface Props {
    courseId: number;
}

export default function CourseCard({ courseId }: Props) {
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));

    type Stage = "notBought" | "boughtInPart" | "boughtInFull";

    const stageOfCourse = (): Stage => {
        if (course?.sections.every(value => value.isAvailable === true)) {
            return "boughtInFull";
        } else if (course?.sections.some(value => value.isAvailable === true)) {
            return "boughtInPart";
        } else {
            return "notBought";
        }
    }

    return (
        <>
            <Card sx={{ width: '90%', maxWidth: '300px', minWidth: '250px' }}>
                <CardMedia
                    sx={{ height: 140 }}
                    image="https://lifeimg.pravda.com/images/doc/8/2/8252751-vipdesignusa-depositphotos.jpg"
                    title="green iguana"
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {course?.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {course?.description}
                    </Typography>
                    <Typography variant="h6" sx={{ mt: '5px' }}>
                        Тривалість: {course?.duration} місяців <br />
                        Ціна: <Box component="span" color="secondary.main">{course?.priceMonthly} грн/міс</Box> або {course?.priceFull} грн
                    </Typography>
                </CardContent>
                <CardActions sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <Button component={Link} to={`/course/${course?.id}`} size="small">
                        {stageOfCourse() === "notBought" ? "Теми уроків" : "Проходити курс"}
                    </Button>
                    {/* <Button
                        size="small"
                        variant={stageOfCourse() === "boughtInFull" ? "outlined" : "contained"}
                        // color="success"
                        disabled={stageOfCourse() === "boughtInFull"}
                        style={{
                            backgroundColor: stageOfCourse() === "boughtInFull" ? '#f0f7f0' : '',
                            borderColor: stageOfCourse() === "boughtInFull" ? '#89b38c' : '',
                            color: stageOfCourse() === "boughtInFull" ? '#6da670' : '',
                            opacity: 1
                        }}
                        >
                        {stageOfCourse() === "notBought" ? "Купити" : stageOfCourse() === "boughtInPart" ? "Купити наступний місяць" : "Куплено"}
                    </Button> */}
                </CardActions>
            </Card >
        </>
    )
}
