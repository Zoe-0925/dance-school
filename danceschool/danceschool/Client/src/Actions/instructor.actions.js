export const APPEND_INSTRUCTORS = 'APPEND_INSTRUCTORS'
export const ADD_INSTRUCTOR = 'ADD_INSTRUCTOR'
export const DELETE_INSTRUCTOR = 'DELETE_INSTRUCTOR'
export const UPDATE_INSTRUCTOR = 'UPDATE_INSTRUCTOR'

export const appendInstructors = data => ({
  type: APPEND_INSTRUCTORS,
  data: data
})
