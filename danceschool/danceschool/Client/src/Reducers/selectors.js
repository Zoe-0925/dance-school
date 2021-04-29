import { createSelector } from 'reselect'

export const selectUserReducer = state => state.UserReducer

export const selectInstructorReducer = state => state.InstructorReducer

export const selectCurrentUserEmail = createSelector(
  selectUserReducer,
  reducer => reducer.email
)

export const selectInstructors = createSelector(
  selectInstructorReducer,
  reducer => reducer.instructors
)

export const selectInstructorOptions = createSelector(
  selectInstructors,
  instructors =>
    instructors.map(each => ({
      label: each.firstName + ' ' + each.lastName,
      value: each.id
    }))
)
