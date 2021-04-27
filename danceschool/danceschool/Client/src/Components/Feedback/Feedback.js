import React from 'react'
import Snackbar from '@material-ui/core/Snackbar'
import MuiAlert from '@material-ui/lab/Alert'
import { DialogCloseIcon } from '../Buttons/IconButtons'

const Feedback = ({ open, message, severity = 'success', handleClose }) => {
  const Alert = props => {
    return <MuiAlert elevation={6} variant='filled' {...props} />
  }

  return (
    <Snackbar
      open={open}
      autohideduration={3000}
      action={<DialogCloseIcon handleClick={handleClose} />}
      onClose={handleClose}
    >
      <Alert autohideduration={3000} severity={severity}>
        {message}
      </Alert>
    </Snackbar>
  )
}

export default Feedback
