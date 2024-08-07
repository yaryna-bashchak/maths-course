import { Typography, List, ListItem, ListItemText } from '@mui/material';
import { UseControllerProps, useController } from 'react-hook-form';
import useCourse from '../../app/hooks/useCourse';
import { useEffect } from 'react';

interface Props extends UseControllerProps {
  sectionId: number | null,
}

export default function Review({ sectionId, ...otherProps }: Props) {
  const { course } = useCourse();
  const { field } = useController({ ...otherProps, defaultValue: null })
  const section = course ? course.sections.find(section => section.id === sectionId) : null;

  useEffect(() => {
    const total = sectionId ? course?.priceMonthly : course?.priceFull;
    field.onChange(total);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <Typography variant="h6" gutterBottom>
        Підсумок замовлення
      </Typography>
      <List disablePadding>
        <ListItem key={course?.id} sx={{ py: 1, px: 0 }}>
          <ListItemText primary={<Typography sx={{ fontSize: '16px' }} variant="h5">{sectionId ? `${section?.title} з курсу "${course?.title}"` : `Курс "${course?.title}"`}</Typography>} />
          <Typography sx={{ fontSize: '16px' }} variant="h5">{sectionId ? course?.priceMonthly : course?.priceFull} грн</Typography>
        </ListItem>
        <ListItem sx={{ py: '0px', px: 0 }}>
          <ListItemText primary={<Typography sx={{ fontSize: '16px' }} variant="h5">Сума</Typography>} />
          <Typography variant="h5" sx={{ fontSize: '16px', fontWeight: 700 }}>
            {field.value} грн
          </Typography>
        </ListItem>
      </List>
    </>
  );
}
