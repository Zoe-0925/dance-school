import { useState } from 'react'

export const useFeedback = () => {
  const [open, setOpen] = useState(false)
  const [message, setMessage] = useState('')
  const [severity, setSeverity] = useState('success')

  const showWarning = (message = 'Something went wrong. Please try again.') => {
    setSeverity('error')
    setMessage(message)
    setOpen(true)
  }

  const showSuccess = () => {
    setSeverity('success')
    setMessage('Successful!')
    setOpen(true)
  }

  const closeFeedback = () => {
    setOpen(false)
  }

  return { open, message, severity, showWarning, showSuccess, closeFeedback }
}
