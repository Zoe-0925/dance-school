import React from 'react'
import { Form, withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { FormTextField } from './FormFields'
import { useButtonStyles } from '../Util'

export const LoginForm = ({
  values,
  handleChange,
  handleSubmit,
  onGoogleLogin,
  goToSignUp
}) => {
  const classes = useButtonStyles()

  return (
    <>
      <div align='center'>
        <Typography variant='h5'>Login</Typography>
      </div>
      <Form onSubmit={handleSubmit}>
        <div align='center' className='auth-form'>
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
            Login
          </Button>
          <br />
          <br />
          <Button
            className='auth-btn'
            classes={{
              root: classes.root, // class name, e.g. `classes-nesting-root-x`
              label: classes.label // class name, e.g. `classes-nesting-label-x`
            }}
            onClick={onGoogleLogin}
          >
            Login With Google
          </Button>
          <p onClick={goToSignUp}>New here? Sign up</p>
        </div>
      </Form>
    </>
  )
}

const LoginFormWithFormik = withFormik({
  mapPropsToValues: ({ email = '', password = '' }) => ({
    email: email,
    password: password
  }),

  // Custom sync validation
  validate: values => {
    const errors = {}
    if (!values.email || values.email === '') {
      errors.email = 'Required'
    }
    if (!values.password || values.password === '') {
      errors.password = 'Required'
    }
    return errors
  },
  handleSubmit: (values, { props: { onContinue } }) => {
    onContinue({
      email: values.email,
      password: values.password
    })
  },
  displayName: 'BasicForm'
})(LoginForm)

export default LoginFormWithFormik
