import {
  LOGIN,
  LOGOUT,
  CLEAR,
  UPDATE_MEMBERSHIP
} from '../Actions/user.actions'

const initialState = {
  email: '',
  role: '',
  token: '',
  studentId: '',
  userName: '',
  membership: ''
}

const UserReducer = (state = initialState, action) => {
  switch (action.type) {
    case LOGIN:
      return {
        email: action.data.email,
        role: action.data.role,
        token: action.data.idToken,
        userName: action.data.userName,
        studentId: action.data.id,
        membership: action.data.membership
      }
    case LOGOUT:
      return initialState
    case UPDATE_MEMBERSHIP:
      return {
        email: state.email,
        role: state.role,
        token: state.token,
        userName: state.userName,
        studentId: state.studentId,
        membership: action.data
      }
    case CLEAR:
      return initialState
    default:
      return state
  }
}

export default UserReducer
