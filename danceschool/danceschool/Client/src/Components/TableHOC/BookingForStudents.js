import React, { Suspense } from 'react'
import {
  fetchStudentBooking,
  fetchCancelBooking,
  fetchCreateBooking
} from '../API/Booking'
import { useFetchPagination } from '../Hooks/useFetchPagination'
import { useFeedback } from '../Hooks/useFeedback'
import { bookingHeader } from '../Table/TableHeaders'
import Pagination from '@material-ui/lab/Pagination'
const Table = React.lazy(() => import('../Table/Table'))
const BookingBody = React.lazy(() => import('../Table/BookingBody'))
const Feedback = React.lazy(() => import('../Feedback/Feedback'))

export default function BookingForStudent ({
  count = 0,
  token = '',
  role = '',
  studentId = ''
}) {
  const {
    open,
    message,
    severity,
    showWarning,
    showSuccess,
    closeFeedback
  } = useFeedback()

  const { data, deleteItem, handleChangePage } = useFetchPagination(
    token,
    role,
    fetchStudentBooking,
    fetchCreateBooking,
    () => {},
    fetchCancelBooking,
    showWarning,
    showSuccess,
    studentId
  )

  return (
    <Suspense fallback={<div>loading...</div>}>
      {data.values && data.values[data.page] ? (
        <>
          <Table tableHeader={bookingHeader}>
            <BookingBody
              bookings={data.values[data.page]}
              handlecancel={deleteItem}
            />
          </Table>
          <Pagination
            count={Math.ceil(count / 10)}
            page={data.page}
            onChange={handleChangePage}
            shape='rounded'
          />
        </>
      ) : (
        <div>You don't have any booking.</div>
      )}
      {open && (
        <Feedback
          open={open}
          message={message}
          severity={severity}
          handleClose={closeFeedback}
        />
      )}
    </Suspense>
  )
}
