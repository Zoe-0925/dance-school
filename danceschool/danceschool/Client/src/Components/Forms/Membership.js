import React, { Fragment } from 'react'
import { withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { DialogCloseIcon } from '../Buttons/IconButtons'
import { FormSelectField, FormTextField } from './FormFields'

export const MembershipForm = ({
  values,
  handleChange,
  handleSubmit,
  setFieldValue,
  durationOptions,
  buttonLabel,
  onCancel
}) => {
  return (
    <Fragment>
      <DialogCloseIcon handleClose={onCancel} />
      <div align='center'>
        <Typography variant='h5'>
          {buttonLabel === 'Create' ? 'Create a ' : 'Update the '}Membership
        </Typography>
      </div>
      <div align='center' className='form'>
        <FormTextField
          id='name'
          inputLabel='Name *'
          value={values.name}
          handleChange={handleChange}
        />
        <FormSelectField
          id='duration'
          inputLabel='Duration *'
          options={durationOptions}
          defaultValue={values.duration}
          handleChange={e => {
            setFieldValue('duration', e.value)
          }}
        />
        <FormTextField
          id='price'
          type='number'
          inputLabel='Price *'
          value={values.price}
          handleChange={handleChange}
        />
        <br />
        <br />
        <Button
          className='navbar-create-btn'
          onClick={() => handleSubmit(values)}
        >
          {buttonLabel}
        </Button>
      </div>
    </Fragment>
  )
}

const MembershipFormWithFormik = withFormik({
  //item = { name: "", price: "", duration: "" }
  mapPropsToValues: ({ item }) => ({
    name: item ? item.name : '',
    duration: item ? item.duration : '',
    price: item ? item.price : ''
  }),

  // Custom sync validation
  validate: values => {
    const errors = {}
    if (!values.name || values.name === '') {
      errors.name = 'Required'
    }
    if (!values.price || values.price === 0) {
      errors.price = 'Required'
    }
    return errors
  },
  handleSubmit: (values, { props: { onContinue, durationOptions } }) => {
    if (values.duration === '') {
      values.duration = durationOptions[0].value
    }
    onContinue(values)
  },
  displayName: 'BasicForm'
})(MembershipForm)

export default MembershipFormWithFormik
