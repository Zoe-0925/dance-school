import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchBookings = async (page, token) =>
  (
    await axios.get(
      base + 'api/bookings/page/' + page + '/size/8/',
      createHeader(token)
    )
  ).data

export const fetchCreateBooking = async (data, token) =>
  (await axios.post(base + 'api/bookings/', data, createHeader(token))).data

export const fetchCancelBooking = async (id, token, email = '') =>
  (
    await axios.delete(
      base + 'api/bookings/' + id + '/student/' + email,
      createHeader(token)
    )
  ).data

export const fetchStudentBooking = async (page = 1, token, id) =>
  (
    await axios.get(
      base + 'api/bookings/student/' + id + '/page/' + page + '/size/8/',
      createHeader(token)
    )
  ).data.data

export const fetchSearchBookingByName = async (query, token) =>
  (await axios.get(base + 'api/bookings/search/' + query, createHeader(token)))
    .data.data

export const fetchSearchBookingByDateRange = async (
  startDate,
  endDate,
  token
) =>
  (
    await axios.post(
      base + 'api/bookings/search/date/',
      {
        startDate: startDate,
        endDate: endDate
      },
      createHeader(token)
    )
  ).data.data
