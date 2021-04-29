import axios from 'axios'
import { createHeader } from '../Util'
import { auth } from '../../firebase'
import firebase from 'firebase/app'
import md5 from 'md5'

const base = process.env.REACT_APP_API_BASE + '/'

export const handleFirebaseError = error => {
  var errorCode = error.code
  var errorMessage = error.message
  if (errorCode === 'auth/wrong-password') {
    alert('Wrong password.')
  } else {
    alert(errorMessage)
  }
}

export const fetchLogin = async data => {
  try {
    await auth.signInWithEmailAndPassword(data.email, md5(data.password))
    return await getRoleAndToken()
  } catch (error) {
    handleFirebaseError(error)
  }
}

export const fetchSignUp = async data => {
  try {
    return await axios.post(
      base + 'Account/sign-up',
      { ...data, password: md5(data.password) },
      createHeader('sign up')
    ) //Send data to the backend to assign a "student" role
  } catch (error) {
    handleFirebaseError(error)
  }
}

export const getRoleAndToken = async () => {
  const idToken = await auth.currentUser.getIdToken()
  if (!idToken || idToken === '') {
    return
  }
  const role =
    (await axios.post(base + 'Account/verify', { idToken: idToken })) || ''
  console.log('role verified', role)
  return { role: role.data, idToken: idToken }
}

export const fetchGoogleLogin = async data => {
  try {
    var provider = new firebase.auth.GoogleAuthProvider()
    provider.addScope('https://www.googleapis.com/auth/contacts.readonly')
    await auth.signInWithPopup(provider)
    // This gives you a Google Access Token. You can use it to access the Google API.
    //var token = result.credential.accessToken
    //var user = result.user
    return await getRoleAndToken()
  } catch (error) {
    handleFirebaseError(error)
  }
}
