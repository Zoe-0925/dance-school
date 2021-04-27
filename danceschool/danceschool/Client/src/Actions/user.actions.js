import { fetchLogin, fetchSignUp } from '../Components/API/Auth'
import { fetchStudentByEmail } from '../Components/API/Student'
import { fetchSubscribe } from '../Components/API/Subscription'
import { convertDuration } from '../Components/Util'
import { add } from 'date-fns'

export const LOGIN = 'LOGIN'
export const SIGNUP = 'SIGNUP'
export const LOGOUT = 'LOGOUT'
export const ADD_OTHER_USERS = 'ADD_OTHER_USERS'
export const CLEAR = 'CLEAR'
export const SAVE_ACCOUNT = 'SAVE_ACCOUNT'
export const UPDATE_MEMBERSHIP = 'UPDATE_MEMBERSHIP'

export const login = data => ({
  type: LOGIN,
  data: data
})

export const signUp = data => ({
  type: SIGNUP,
  data: data
})

export const logOut = () => ({
  type: LOGOUT
})

export const updateMembership = data => ({
  type: UPDATE_MEMBERSHIP,
  data: data
})

export const loginThunk = data => async dispatch => {
  const account = await fetchLogin({
    email: data.email,
    password: data.password
  })
console.log("account", account)
  if (account && account.role === 'admin') {
    dispatch({
      type: LOGIN,
      data: { ...account, email: data.email, userName: data.userName }
    })
    return { role: account.role }
  }
  if (account && account.role === 'student') {
    const response = await fetchStudentByEmail(data.email, data.idToken)
    console.log('student response', response)
    if (response.data) {
      dispatch({
        type: LOGIN,
        data: {
          ...account,
          email: data.email,
          userName: data.userName,
          id: response.data.id,
          membership: response.data.membership
        }
      })
      return { role: account.role }
    }
  }
}

export const signUpThunk = data => async dispatch => {
  const account = await fetchSignUp({
    email: data.email,
    password: data.password,
    userName: data.userName
  })
  if (account) {
    dispatch({
      type: SIGNUP,
      data: { ...account, email: data.email }
    })
    return { ...account, email: data.email }
  }
}

export const subscribe = (membership, studentId, token) => async dispatch => {
  const today = new Date()
  const nextBillingDate = add(today, convertDuration(membership.duration))
  const response = await fetchSubscribe(
    {
      membershipId: membership.id,
      studentID: studentId,
      membershipName: membership.name,
      startDate: today,
      nextBillingDate: nextBillingDate
    },
    token
  )
  if (response.data) {
    dispatch(updateMembership(membership))
    return true
  }
}
