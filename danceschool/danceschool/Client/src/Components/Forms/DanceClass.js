import React, { Fragment } from 'react'
import { Form } from 'formik'
import { withFormik } from 'formik'
import { Typography, Button } from '@material-ui/core'
import { DialogCloseIcon } from '../Buttons/IconButtons'
import { FormTextField, StartEndTimeFields } from './FormFields'

export const DanceClassForm = ({
  values,
  handleSubmit,
  setFieldValue,
  onCancel,
  buttonLabel,
  handleChange
}) => (
  <Fragment>
    <DialogCloseIcon handleClose={onCancel} />
    <div align='center'>
      <Typography variant='h5'>
        {buttonLabel === 'Create' ? 'Create a ' : 'Update the '}DanceClass
      </Typography>
    </div>
    <Form onSubmit={handleSubmit}>
      <div align='center' className='form'>
        <FormTextField
          id='course'
          inputLabel='Course'
          value={values.course}
          readOnly={buttonLabel === 'Update'}
          handleChange={handleChange}
        />
        <StartEndTimeFields
          startTime={values.startTime}
          endTime={values.endTime}
          setFieldValue={setFieldValue}
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

export const DanceClassFormWithFormik = withFormik({
  mapPropsToValues: ({ item }) => ({
    course: item ? item.courseName : '',
    startTime: item ? item.startTime : new Date(),
    endTime: item ? item.endTime : new Date(),
    courseId: item ? item.courseId || item.id : ''
  }),

  validate: values => {
    const errors = {}
    if (values.course === '') {
      errors.course = 'Please enter a course name'
    }
    if (values.startTime > values.EndTime) {
      errors.startTime = 'Start Time can not be after the End Time'
      errors.endTime = 'Start Time can not be after the End Time'
    }
    return errors
  },
  handleSubmit: (values, { props: { onContinue } }) => {
    onContinue(values)
  }
})(DanceClassForm)

const DanceClassHOC = ({
  view,
  setMainView,
  item,
  createDanceClass,
  updateDanceClass
}) => {
  const handleContinue = value => {
    view === 'createDanceClass'
      ? createDanceClass(value)
      : updateDanceClass(value)
  }

  return (
    <DanceClassFormWithFormik
      item={view === 'updateDanceClass' && item}
      onContinue={handleContinue}
      onCancel={setMainView}
      buttonLabel={view === 'updateDanceClass' ? 'Update' : 'Create'}
    />
  )
}

export default DanceClassHOC
