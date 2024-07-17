import { Typography, Grid, FormHelperText } from "@mui/material";
import SelectableCard from "./SelectableCard";
import { useState } from "react";
import { UseControllerProps, useController } from "react-hook-form";
import useCourse from "../../app/hooks/useCourse";

interface Props extends UseControllerProps {}

export default function PurchasePlanSelection({ ...otherProps }: Props) {
  const { course } = useCourse();

  const { fieldState, field } = useController({ ...otherProps, defaultValue: null, rules: { required: 'Будь ласка, оберіть план' } })
  const [selectedCard, setSelectedCard] = useState<string | null>(field.value);

  const handleCardClick = (type: string) => {
    setSelectedCard(type);
    field.onChange(type);
  };

  const cards = [
    {
      title: 'Оплачувати частинами',
      description: `
      Курс "${course?.title}" складається з ${course?.duration} місяців навчання.
      При оплаті певного місяця вам відкривається доступ до відповідних уроків.
      Оплата наступних місяців не є обов'язковою, але ви матимете доступ лише до тих, що оплатили.`,
      price: `${course?.priceMonthly} грн/міс`,
      type: 'monthly',
    },
    {
      title: 'Весь курс одразу',
      description: `
      При повній оплаті ви отримуєте доступ до всіх уроків та одразу можете перейти до вивчення будь-якої теми. При купівлі одразу всього курсу ціна знижена.`,
      price: `${course?.priceFull} грн`,
      oldPrice: `${course?.priceMonthly && course?.duration && course.priceMonthly * course.duration} грн`,
      type: 'full',
    },
  ];

  return (
    <>
      <Typography variant="h6" gutterBottom>
        Оберіть план
      </Typography>
      <Grid container spacing={3}>
        {cards.map((card, index) => (
          <Grid item xs={12} sm={6} key={index}>
            <SelectableCard
              title={card.title}
              description={card.description}
              price={card.price}
              oldPrice={card.oldPrice}
              selected={selectedCard === card.type}
              onClick={() => handleCardClick(card.type)}
            />
          </Grid>
        ))}
      </Grid>
      {fieldState.error && <FormHelperText sx={{fontSize: '12px'}} error>{fieldState.error.message}</FormHelperText>}
    </>
  );
}
