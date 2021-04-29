import React, { Fragment } from 'react'
import { Form } from 'formik'
import { withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { FormTextField, FormSelectField } from './FormFields'

import { getClassOptions, getWeekDayfromDate } from '../Util'

export const BookingForm = ({
  values,
  handleChange,
  handleSubmit,
  setFieldValue,
  item
}) => {
  const dateOptions = getClassOptions(item.startDate, item.numberOfOccurence)

  return (
    <Fragment>
      <div align='center'>
        <Typography variant='h5'>Book Dance Classes</Typography>
      </div>
      <Form onSubmit={handleSubmit}>
        <div align='center' className='form'>
          <FormTextField
            id='name'
            inputLabel='Class'
            value={values.name}
            readOnly={true}
          />
          <FormTextField
            id='time'
            inputLabel='Time Each Week'
            value={
              getWeekDayfromDate(values.startDate) +
              ' ' +
              values.startTime +
              ':00-' +
              values.endTime +
              ':00'
            }
            readOnly={true}
          />
          <FormTextField
            id='studentName'
            inputLabel='Your Name *'
            value={values.studentName}
            handleChange={handleChange}
          />
          <FormSelectField
            id='date'
            inputLabel='Select dates *'
            value={values.date}
            handleChange={value => {
              handleChange(value)
              setFieldValue('numberOfDates', value.length)
            }}
            options={dateOptions}
            isMulti={true}
          />
          <FormTextField
            id='danceClassName'
            inputLabel='Class'
            readOnly={true}
            value={values.danceClassName}
            handleChange={handleChange}
          />
          <FormTextField
            id='price'
            inputLabel='Total (AUD)'
            readOnly={true}
            value={values.price}
          />
          <br />
          <br />
          <Button className='navbar-create-btn' onClick={handleSubmit}>
            Book Now
          </Button>
        </div>
      </Form>
    </Fragment>
  )
}

const BookingFormWithFormik = withFormik({
  mapPropsToValues: ({ item }) => ({
    name: item.name,
    classOptions: getClassOptions(item.startDate),
    price: item.price,
    studentName: '',
    date: ''
  }),

  // Custom sync validation
  validate: values => {
    const errors = {}
    if (!values.studentName || values.studentName === '') {
      errors.studentName = 'Required'
    }
    if (!values.date || values.date === '') {
      errors.date = 'Required'
    }
    if (!values.danceClassName || values.danceClassName === '') {
      errors.danceClassName = 'Required'
    }
    return errors
  },
  handleSubmit: (values, { props: { onContinue } }) => {
    onContinue({
      studentName: values.studentName,
      date: values.date
    })
  },
  displayName: 'BasicForm'
})(BookingForm)

export default BookingFormWithFormik
