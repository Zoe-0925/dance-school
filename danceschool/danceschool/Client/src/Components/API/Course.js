import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchCourses = async page =>
  (await axios.get(base + 'api/courses/page/' + page + '/size/8')).data

export const fetchCoursesWithCount = async (page, token) =>
  (
    await axios.get(
      base + 'api/courses/page/' + page + '/size/8/count',
      createHeader(token)
    )
  ).data

export const fetchCreateCourse = async (data, token) =>
  await axios.post(base + 'api/courses/', data, createHeader(token))

export const fetchUpdateCourse = async (data, token) =>
  await axios.put(base + 'api/courses/', data, createHeader(token))

export const fetchDeleteCourse = async (id, token) =>
  await axios.delete(base + 'api/courses/' + id, createHeader(token))

export const fetchSearchCourseByName = async (query, token) =>
  (
    await axios.get(
      base + 'api/courses/search/' + query,
      createHeader(token)
    )
  ).data.data
