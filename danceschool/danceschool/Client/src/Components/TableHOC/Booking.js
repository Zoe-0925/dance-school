import React, { Suspense } from 'react'
import {
  fetchBookings,
  fetchCancelBooking,
  fetchCreateBooking,
  fetchSearchBookingByName
} from '../API/Booking'
import { useFetchPagination } from '../Hooks/useFetchPagination'
import { useSearch } from '../Hooks/useSearch'
import { bookingHeader } from '../Table/TableHeaders'
import Pagination from '@material-ui/lab/Pagination'
import SearchBar from 'material-ui-search-bar'

const Table = React.lazy(() => import('../Table/Table'))
const BookingBody = React.lazy(() => import('../Table/BookingBody'))

export default function Booking ({
  count = 0,
  token = '',
  role = '',
  showWarning,
  showSuccess
}) {
  const {
    data,
    item,
    setItem,
    view,
    setView,
    create,
    update,
    deleteItem,
    handleChangePage
  } = useFetchPagination(
    token,
    role,
    fetchBookings,
    fetchCreateBooking,
    () => {},
    fetchCancelBooking,
    showWarning,
    showSuccess
  )

  const [state, setState, handleSearch] = useSearch(
    fetchSearchBookingByName,
    token
  )

  return (
    <Suspense fallback={<div>loading...</div>}>
      <SearchBar
        value={state.value}
        onChange={newValue => setState({ value: newValue })}
        onRequestSearch={handleSearch}
      />
      <Table tableHeader={bookingHeader}>
        <BookingBody
          bookings={!state.data?data.values[data.page]:state.data}
          handlecancel={deleteItem}
        />
      </Table>
      <Pagination
        count={Math.ceil(count / 10)}
        page={data.page}
        onChange={handleChangePage}
        shape='rounded'
      />
    </Suspense>
  )
}
