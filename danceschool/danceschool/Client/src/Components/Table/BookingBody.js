import React from 'react'
import { TableRow } from '@material-ui/core'
import { TableDeleteIconButton } from '../Buttons/IconButtons'
import TableCell from './TableCell'
import { v4 as uuidv4 } from 'uuid'
import { formatDate, formatTime } from '../Util'

const BookingTableBody = ({ bookings = [], handleDelete, role = '' }) => (
  <>
    {role === 'student' && (
      <p className='title'>
        Go to the Classes Tab in the left to book a class.{' '}
      </p>
    )}
    {bookings.length > 0 ? (
      bookings.map(booking => (
        <TableRow key={booking.id} className='table-body'>
          {[
            formatDate(booking.date) + ' - ' + formatTime(booking.date),
            formatDate(booking.bookingDate),
            booking.courseName,
            booking.studentEmail
          ].map(each => (
            <TableCell label={each} key={uuidv4()} />
          ))}
          <TableDeleteIconButton onClick={() => handleDelete(booking.id)} />
        </TableRow>
      ))
    ) : (
      <TableRow></TableRow>
    )}
  </>
)

export default BookingTableBody
