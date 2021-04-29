import React from 'react'
import { Form, withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { FormTextField } from './FormFields'
import { useButtonStyles } from '../Util'

export const SignUpForm = ({
  values,
  handleChange,
  handleSubmit,
  goToLogin
}) => {
  const classes = useButtonStyles()

  return (
    <>
      <div align='center'>
        <Typography variant='h5'>Sign Up</Typography>
      </div>
      <Form onSubmit={handleSubmit}>
        <div align='center' className='form'>
          <FormTextField
            id='userName'
            inputLabel='User Name *'
            value={values.userName}
            handleChange={handleChange}
            size='medium'
            className='auth-field'
          />
          <FormTextField
            id='email'
            inputLabel='Email *'
            value={values.email}
            handleChange={handleChange}
            size='medium'
            className='auth-field'
          />
          <FormTextField
            id='password'
            inputLabel='Password'
            value={values.password}
            handleChange={handleChange}
            type='password'
            size='medium'
            className='auth-field'
          />
          <br />
          <br />
          <Button
            className='auth-btn'
            classes={{
              root: classes.root, // class name, e.g. `classes-nesting-root-x`
              label: classes.label // class name, e.g. `classes-nesting-label-x`
            }}
            onClick={handleSubmit}
          >
            Sign Up
          </Button>
          <p onClick={goToLogin}>Already have an account? Login</p>
        </div>
      </Form>
    </>
  )
}

const SignUpFormWithFormik = withFormik({
  // Custom sync validation
  validate: values => {
    const errors = {}
    if (!values.userName || values.userName === '') {
      errors.userName = 'Required'
    }
    if (!values.email || values.email === '') {
      errors.email = 'Required'
    }
    if (!values.password || values.password === '') {
      errors.password = 'Required'
    }
    if (
      !values.password.match(
        '^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|]).{8,32}$'
      )
    ) {
      errors.password = 'The password must be at least 8 digits with 1 number, 1 alphabet and 1 special character'
    }
    return errors
  },
  handleSubmit: (values, { props: { onContinue } }) => {
    onContinue(values)
  },
  displayName: 'BasicForm'
})(SignUpForm)

export default SignUpFormWithFormik
