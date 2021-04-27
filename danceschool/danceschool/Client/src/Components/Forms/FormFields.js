import React from 'react'
import Select from 'react-select'
import InputLabel from '@material-ui/core/InputLabel'
import { Field } from 'formik'
import { TextField } from 'formik-material-ui'
import DateFnsUtils from '@date-io/date-fns'
import {
  MuiPickersUtilsProvider,
  KeyboardDateTimePicker
} from '@material-ui/pickers'

export const FieldContainer = ({ id, inputLabel, ...props }) => (
  <div className='field-container'>
    <InputLabel className='label' id={id}>
      {inputLabel}
    </InputLabel>
    {props.children}
  </div>
)

export const FormSelectField = ({
  id,
  inputLabel,
  options,
  handleChange,
  defaultValue = '',
  isMulti = false
}) => (
  <FieldContainer id={id} inputLabel={inputLabel}>
    <Select
      name={id}
      className='select'
      defaultValue={defaultValue !== '' ? defaultValue : options[0]}
      options={options}
      onChange={handleChange}
      isMulti={isMulti}
    />
  </FieldContainer>
)

export const FormTextField = ({
  id = '',
  type = 'text',
  inputLabel = '',
  value = '',
  handleChange,
  readOnly = false,
  size = 'small',
}) => {
  return (
    <FieldContainer id={id} inputLabel={inputLabel}>
      <Field
        className='field' 
        component={TextField}
        name={id}
        type={type}
        variant='outlined'
        fullWidth={true}
        size={size}
        onChange={handleChange}
        value={value}
        margin='normal'
        readOnly={readOnly}
      />
    </FieldContainer>
  )
}

export const FormDateTimePicker = ({
  id,
  value = new Date(),
  handleChange,
  inputLabel = ''
}) => {
  return (
    <FieldContainer id={id} inputLabel={inputLabel}>
      <MuiPickersUtilsProvider utils={DateFnsUtils}>
        <KeyboardDateTimePicker
          variant='outlined'
          className='field'
          ampm={true}
          label='With keyboard'
          margin='normal'
          value={value}
          id={id}
          onChange={date => handleChange(date)}
          onError={console.log}
          disablePast={false}
          format='MM/dd/yyyy HH:mm'
        />
      </MuiPickersUtilsProvider>
    </FieldContainer>
  )
}

export const StartEndTimeFields = ({
  startTime,
  endTime,
  setFieldValue,
  startTimeName = '',
  endTimeName = ''
}) => (
  <>
    <FormDateTimePicker
      id={startTimeName ? startTimeName : 'startTime'}
      inputLabel='Start Time *'
      value={startTime}
      handleChange={value => setFieldValue('startTime', value)}
    />
    <FormDateTimePicker
      id={endTimeName ? endTimeName : 'endTime'}
      inputLabel='End Time*'
      value={endTime}
      handleChange={value => setFieldValue('endTime', value)}
    />
    <br />
    <br />
  </>
)

/** 
export const FormDatePicker = ({
  id,
  value = new Date(),
  handleChange,
  inputLabel = ''
}) => {
  return (
    <FieldContainer id={id} inputLabel={inputLabel}>
      <MuiPickersUtilsProvider utils={DateFnsUtils}>
        <KeyboardDatePicker
          variant='outlined'
          className='field'
          disableToolbar
          format='MM/dd/yyyy'
          margin='normal'
          id={id}
          value={value}
          onChange={date => handleChange(date)}
          KeyboardButtonProps={{
            'aria-label': 'change date'
          }}
        />
      </MuiPickersUtilsProvider>
    </FieldContainer>
  )
}*/
