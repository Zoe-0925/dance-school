import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const registerInstructor = async (data, token) =>
  await axios.post(base + 'api/instructors/', data, createHeader(token)).data

export const unRegisterInstructor = async (id, token) =>
  await axios.delete(base + 'api/instructors/' + id, createHeader(token)).data

export const fetchInstructors = async () =>
  (await axios.get(base + 'api/instructors/')).data.data

export const fetchUpdateInstructor = async (data, token) =>
  (await axios.put(base + 'api/instructors/', data, createHeader(token))).data

export const fetchSearchInstructorByName = async (query, token) =>
  (
    await axios.get(
      base + 'api/instructors/search/' + query,
      createHeader(token)
    )
  ).data.data
