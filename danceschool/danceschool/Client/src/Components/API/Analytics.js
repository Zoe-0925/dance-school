import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchAnalytics = async token =>
  (await axios.get(base + 'api/analytics/', createHeader(token))).data.data

export const fetchBookingCount = async (type, token) =>
  (
    await axios.get(
      base + 'api/analytics/bookings/count/' + type,
      createHeader(token)
    )
  ).data.data
