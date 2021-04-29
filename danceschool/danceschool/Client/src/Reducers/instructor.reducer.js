import {
  APPEND_INSTRUCTORS,
  ADD_INSTRUCTOR,
  DELETE_INSTRUCTOR,
  UPDATE_INSTRUCTOR
} from '../Actions/instructor.actions'

const InstructorReducer = (state = { instructors: [] }, action) => {
  switch (action.type) {
    case APPEND_INSTRUCTORS:
      const count = action.data.length
      return { instructors: action.data, count: count }
    case ADD_INSTRUCTOR:
      return { instructors: [...state.instructors, action.data] }
    case DELETE_INSTRUCTOR:
      return {
        instructors: [...state.instructors].filter(i => i.id !== action.id)
      }
    case UPDATE_INSTRUCTOR:
      let newState = { ...state }
      newState.instructors = newState.instructors.filter(
        i => i.id !== action.data.id
      )
      return { instructors: [...newState.instructors, action.data] }
    default:
      return state
  }
}

export default InstructorReducer
