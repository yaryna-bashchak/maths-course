import { Card, CardActionArea, CardContent, Typography } from "@mui/material";

interface Props {
    title: string,
    description: string,
    price: string,
    oldPrice?: string,
    selected: boolean,
    onClick: () => void,
}

export default function SelectableCard({ title, description, price, oldPrice, selected, onClick }: Props) {

    return (
        <Card
            sx={{
                border: selected ? '2px solid #3f51b5' : '2px solid #ccc',
                boxShadow: selected ? '0 0 10px rgba(0, 0, 0, 0.2)' : '0 0 5px rgba(0, 0, 0, 0.1)',
                backgroundColor: selected ? '#e3f2fd' : '#ffffff',
                height: '100%'
            }}
        >
            <CardActionArea onClick={onClick} sx={{height: '100%'}}>
                <CardContent sx={{display: 'flex', flexDirection: 'column', justifyContent: 'space-between', height: '100%', gap: '10px'}}>
                    <Typography variant="h5" component="div">
                        {title}
                    </Typography>
                    <Typography variant="body1" color="text.secondary">
                        {description}
                    </Typography>
                    <Typography variant="h5" component="div">
                        {oldPrice && <span style={{ textDecoration: 'line-through', marginRight: '8px' }}>
                            {oldPrice}
                        </span>}
                        {price}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}