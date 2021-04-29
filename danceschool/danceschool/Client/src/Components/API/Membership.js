import axios from 'axios'
import { createHeader } from '../Util'

const base = process.env.REACT_APP_API_BASE + "/"

export const fetchMemberships = async () =>
  (await axios.get(base + 'api/memberships/')).data.data

export const fetchCreateMembership = async (data, token) =>
  (await axios.post(base + 'api/memberships/', data, createHeader(token))).data

export const fetchUpdateMembership = async (data, token) =>
  (await axios.put(base + 'api/memberships/', data, createHeader(token))).data

export const fetchDeleteMembership = async (id, token) =>
  (await axios.delete(base + 'api/memberships/' + id, createHeader(token))).data