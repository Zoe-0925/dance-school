import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchClassByCourse = async (id, page) =>
  (
    await axios.get(
      base +
        'api/classes/course/' +
        id +
        '/page/' +
        page +
        '/size/8/upcoming/' +
        false
    )
  ).data.data

export const fetchUpComingClassByCourse = async (id, page) =>
  (
    await axios.get(
      base +
        'api/classes/course/' +
        id +
        '/page/' +
        page +
        '/size/8/upcoming/' +
        true
    )
  ).data.data

export const fetchCreateClass = async (data, token) =>
  (await axios.post(base + 'api/classes/', data, createHeader(token))).data

export const fetchUpdateClass = async (data, token) =>
  (await axios.put(base + 'api/classes/', data, createHeader(token))).data

export const fetchDeleteClass = async (id, token) =>
  (await axios.delete(base + 'api/classes/' + id, createHeader(token))).data
