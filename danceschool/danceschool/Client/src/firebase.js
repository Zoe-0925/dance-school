import firebase from 'firebase/app'
import 'firebase/auth'
import 'firebase/firestore'

var firebaseConfig = {
  apiKey: process.env.REACT_APP_FIREBASE_APIKEY,
  authDomain: process.env.REACT_APP_FIREBASE_AUTHDOMAIN,
  projectId: process.env.REACT_APP_FIREBASE_PROJECTID,
  storageBucket: process.env.REACT_APP_STORAGEBUCKET,
  messagingSenderId: process.env.REACT_APP_MESSENGINGSENDERID,
  appId: process.env.REACT_APP_APPID
}

const provider = new firebase.auth.GoogleAuthProvider()

firebase.initializeApp(firebaseConfig)



export const auth = firebase.auth()

export const firestore = firebase.firestore()

export const signInWithGoogle = () => {
  auth.signInWithPopup(provider)
}

