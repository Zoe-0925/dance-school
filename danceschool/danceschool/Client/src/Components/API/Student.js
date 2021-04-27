import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchStudents = async (page = 1, token) =>
  (
    await axios.get(
      base + 'api/students/page/' + page + '/size/8',
      createHeader(token)
    )
  ).data

export const fetchStudentByEmail = async (email = '', token) =>
  (await axios.get(base + 'api/students/' + email, createHeader(token))).data

export const unRegisterStudent = async (id, token) =>
  await axios.delete(base + 'api/students/' + id, createHeader(token)).data

export const fetchSearchStudentByName = async (query, token) =>
  (await axios.get(base + 'api/students/search/' + query, createHeader(token)))
    .data.data
