import { Typography, Grid, FormHelperText } from "@mui/material";
import SelectableCard from "./SelectableCard";
import { useState } from "react";
import { UseControllerProps, useController } from "react-hook-form";

interface Props extends UseControllerProps {
  title?: string,
  duration?: number,
  priceFull?: number,
  priceMonthly?: number,
}

export default function PurchasePlanSelection({ title, duration, priceFull, priceMonthly, ...otherProps }: Props) {
  const { fieldState, field } = useController({ ...otherProps, defaultValue: null, rules: { required: 'Будь ласка, оберіть план' } })
  const [selectedCard, setSelectedCard] = useState<number | null>(field.value);

  const handleCardClick = (cardIndex: number) => {
    setSelectedCard(cardIndex);
    field.onChange(cardIndex);
  };

  const cards = [
    {
      title: 'Оплачувати частинами',
      description: `
      Курс "${title}" складається з ${duration} місяців навчання.
      При оплаті певного місяця вам відкривається доступ до відповідних уроків.
      Оплата наступних місяців не є обов'язковою, але ви матимете доступ лише до тих, що оплатили.`,
      price: `${priceMonthly} грн/міс`,
    },
    {
      title: 'Весь курс одразу',
      description: `
      При повній оплаті ви отримуєте доступ до всіх уроків та одразу можете перейти до вивчення будь-якої теми. При купівлі одразу всього курсу ціна знижена.`,
      price: `${priceFull} грн`,
      oldPrice: `${priceMonthly && duration && priceMonthly * duration} грн`,
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
              selected={selectedCard === index}
              onClick={() => handleCardClick(index)}
            />
          </Grid>
        ))}
        {/* <Grid item xs={12} sm={6}>
          <TextField
            required
            id="firstName"
            name="firstName"
            label="First name"
            fullWidth
            autoComplete="given-name"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            required
            id="lastName"
            name="lastName"
            label="Last name"
            fullWidth
            autoComplete="family-name"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            required
            id="address1"
            name="address1"
            label="Address line 1"
            fullWidth
            autoComplete="shipping address-line1"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            id="address2"
            name="address2"
            label="Address line 2"
            fullWidth
            autoComplete="shipping address-line2"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            required
            id="city"
            name="city"
            label="City"
            fullWidth
            autoComplete="shipping address-level2"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            id="state"
            name="state"
            label="State/Province/Region"
            fullWidth
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            required
            id="zip"
            name="zip"
            label="Zip / Postal code"
            fullWidth
            autoComplete="shipping postal-code"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            required
            id="country"
            name="country"
            label="Country"
            fullWidth
            autoComplete="shipping country"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12}>
          <FormControlLabel
            control={<Checkbox color="secondary" name="saveAddress" value="yes" />}
            label="Use this address for payment details"
          />
        </Grid> */}
      </Grid>
      {fieldState.error && <FormHelperText sx={{fontSize: '12px'}} error>{fieldState.error.message}</FormHelperText>}
    </>
  );
}
