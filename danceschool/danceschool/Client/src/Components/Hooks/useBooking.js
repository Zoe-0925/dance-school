import { useState } from 'react'
import { fetchCreateBooking } from '../API/Booking'
import { formatISO } from 'date-fns'

export const useBooking = (role = '', token = '', showWarning, setView) => {
  const [booking, setBooking] = useState()

  const handleBooking = async value => {
    if (role === 'student') {
      let formatValue = {
        ...value,
        date: formatISO(value.date, { representation: 'date' }),
        bookingDate: formatISO(new Date(), { representation: 'date' })
      }
      const response = await fetchCreateBooking(formatValue, token)
      if (response.data) {
        setBooking({ ...value, id: response.data })
        setView('bookingConfirmation')
      } else {
        showWarning('Booking failed. Please try again.')
        setView('main')
      }
    }
  }

  return { booking, handleBooking }
}
