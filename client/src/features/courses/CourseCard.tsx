import { Card, CardMedia, CardContent, Typography, CardActions, Button, Box } from "@mui/material";
import { Course } from "../../app/models/course";
import { Link } from "react-router-dom";

interface Props {
    course: Course;
}

export default function CourseCard({ course }: Props) {
    return (
        <>
            <Card>
                <CardMedia
                    sx={{ height: 140 }}
                    image="https://lifeimg.pravda.com/images/doc/8/2/8252751-vipdesignusa-depositphotos.jpg"
                    title="green iguana"
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {course.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {course.description}
                    </Typography>
                    <Typography variant="h6" sx={{ mt: '5px' }}>
                        Тривалість: {course.duration} місяців <br />
                        Ціна: <Box component="span" color="secondary.main">{course.priceMonthly} грн/міс</Box> або {course.priceFull} грн
                    </Typography>
                </CardContent>
                <CardActions sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <Button component={Link} to={`/course/${course.id}`} size="small">Дізнатись більше</Button>
                    <Button size="small" variant="contained">Купити</Button>
                </CardActions>
            </Card>
        </>
    )
}