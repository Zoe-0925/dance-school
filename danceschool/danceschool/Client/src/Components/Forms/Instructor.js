import React, { Fragment } from 'react'
import { Form } from 'formik'
import { withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { DialogCloseIcon } from '../Buttons/IconButtons'
import { FormTextField } from './FormFields'

export const InstructorForm = ({
  values,
  handleChange,
  handleSubmit,
  setFieldValue,
  onCancel,
  buttonLabel
}) => {
  return (
    <Fragment>
      <DialogCloseIcon handleClose={onCancel} />
      <div align='center'>
        <Typography variant='h5'>
          {buttonLabel === 'Create' ? 'Create an ' : 'Update the '}Instructor
        </Typography>
      </div>
      <Form onSubmit={handleSubmit}>
        <div align='center' className='form'>
          <FormTextField
            id='firstName'
            inputLabel='First Name *'
            value={values.firstName}
            handleChange={handleChange}
          />
          <FormTextField
            id='lastName'
            inputLabel='Last Name *'
            value={values.lastName}
            handleChange={handleChange}
          />
          <FormTextField
            id='email'
            inputLabel='Email *'
            value={values.email}
            handleChange={handleChange}
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
}

const InstructorFormWithFormik = withFormik({
  mapPropsToValues: ({
    item = { firstName: '', lastName: '', email: '', danceClasses: [] }
  }) => ({
    firstName: item.firstName,
    lastName: item.lastName,
    email: item.email,
    danceClasses: item.danceClasses
  }),

  // Custom sync validation
  validate: values => {
    const errors = {}
    if (!values.firstName || values.firstName === '') {
      errors.firstName = 'Required'
    }
    if (!values.lastName || values.lastName === '') {
      errors.lastName = 'Required'
    }
    if (!values.email || values.email === '') {
      errors.email = 'Required'
    }
    return errors
  },
  handleSubmit: (values, { props: { onContinue } }) => {
    onContinue({
      firstName: values.firstName,
      lastName: values.lastName,
      email: values.email,
      danceClasses: values.danceClasses
    })
  },
  displayName: 'BasicForm'
})(InstructorForm)

export default InstructorFormWithFormik
