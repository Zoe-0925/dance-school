import React, { Fragment } from 'react'
import { Form } from 'formik'
import { withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { DialogCloseIcon } from '../Buttons/IconButtons'
import { FormSelectField, FormTextField } from './FormFields'

export const CourseForm = ({
  values,
  handleChange,
  handleSubmit,
  setFieldValue,
  onCancel,
  instructorOptions = [],
  buttonLabel
}) => (
  <Fragment>
    <DialogCloseIcon handleClose={onCancel} />
    <div align='center'>
      <Typography variant='h5'>
        {buttonLabel === 'Create' ? 'Create a ' : 'Update the '} Course
      </Typography>
    </div>
    <Form onSubmit={handleSubmit}>
      <div align='center' className='form'>
        <FormTextField
          id='name'
          inputLabel='Name *'
          value={values.name}
          handleChange={handleChange}
        />
        <FormTextField
          id='price'
          inputLabel='Price'
          value={values.price}
          handleChange={handleChange}
        />
        <FormSelectField
          id='instructorID'
          inputLabel='Instructor *'
          options={instructorOptions}
          handleChange={e => setFieldValue('instructorID', e.value)}
        />
        <FormTextField
          id='bookingLimit'
          inputLabel='Booking Limit *'
          value={values.bookingLimit}
          handleChange={handleChange}
          type='number'
        />
        <br />
        <br />
        <Button className='navbar-create-btn' onClick={handleSubmit}>
          {buttonLabel}
        </Button>
      </div>
    </Form>
  </Fragment>
)

export const CourseFormWithFormik = withFormik({
  mapPropsToValues: ({ item }) => ({
    id: item ? item.id : '',
    name: item ? item.name : '',
    numberOfOccurences: item ? item.numberOfOccurences : '',
    price: item ? item.price : '',
    instructorID: item ? item.instructorID : '',
    bookingLimit: item ? item.bookingLimit : ''
  }),

  // Custom sync validation
  validate: values => {
    const errors = {}
    if (!values.name || values.name === '') {
      errors.name = 'Required'
    }
    if (!values.price || values.price === '') {
      errors.price = 'Required'
    }
    if (!values.price && values.price < 0) {
      errors.price = 'Invalid price'
    }
    if (!values.bookingLimit || values.bookingLimit === '') {
      errors.bookingLimit = 'Required'
    }
    return errors
  },
  handleSubmit: (
    values,
    { props: { onContinue, instructorOptions, item } }
  ) => {
    onContinue({
      id: values.id,
      name: values.name,
      price: values.price,
      instructorID:
        values.instructorID !== ''
          ? values.instructorID
          : instructorOptions[0].value,
      bookingLimit: values.bookingLimit
    })
  },
  displayName: 'BasicForm'
})(CourseForm)

const CourseHOC = ({
  view,
  setMainView,
  item,
  instructorOptions,
  createCourse,
  updateCourse
}) => {
  const handleContinue = value => {
    view === 'createCourse' ? createCourse(value) : updateCourse(value)
  }

  return (
    <CourseFormWithFormik
      item={view === 'updateCourse' && item}
      onContinue={handleContinue}
      onCancel={setMainView}
      instructorOptions={instructorOptions}
      buttonLabel={view === 'updateCourse' ? 'Update' : 'Create'}
    />
  )
}

export default CourseHOC

