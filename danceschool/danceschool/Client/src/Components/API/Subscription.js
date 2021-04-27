import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchSubscriptions = async (page = 1, token) =>
  (
    await axios.get(
      base + 'api/subscription/page/' + page + '/size/8',
      createHeader(token)
    )
  ).data

export const fetchStudentSubscriptions = async (id, page, token) =>
  (
    await axios.get(
      base + 'api/student/' + id + '/history/page/' + page + '/size/8',
      createHeader(token)
    )
  ).data

export const fetchSubscribe = async (data, token) =>
  (await axios.post(base + 'api/subscription/', data, createHeader(token))).data

export const fetchCancelSubscription = async (id, token) =>
  (await axios.delete(base + 'api/subscription/' + id, createHeader(token)))
    .data