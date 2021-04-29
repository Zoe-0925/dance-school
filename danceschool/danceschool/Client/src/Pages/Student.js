import React, { useState, useEffect, Suspense } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import DrawerContainer from '../Components/Drawer/DrawerContainer'
import { selectUserReducer } from '../Reducers/selectors'
import history from '../history'
import { loginThunk, logOut } from '../Actions/user.actions'
import { auth } from '../firebase'
import { useFeedback } from '../Components/Hooks/useFeedback'

const Membership = React.lazy(() => import('../Components/TableHOC/Membership'))
const Course = React.lazy(() => import('../Components/Card/Course'))
const Feedback = React.lazy(() => import('../Components/Feedback/Feedback'))

const Subscription = React.lazy(() =>
  import('../Components/TableHOC/Subscription')
)
const Booking = React.lazy(() =>
  import('../Components/TableHOC/BookingForStudents')
)

const Student = () => {
  const dispatch = useDispatch()
  const [currentPage, setPage] = useState('Courses')
  const account = useSelector(selectUserReducer)
  const role = account ? account.role : ''
  const token = account ? account.token : ''
  const membership = account ? account.membership : ''

  console.log("role", role)

  const {
    open,
    message,
    severity,
    showWarning,
    showSuccess,
    closeFeedback
  } = useFeedback()

  /** 
  useEffect(() => {
    if (role !== 'student') {
      dispatch(logOut)
      history.push('/auth')
    }
    // eslint-disable-next-line
  }, [])*/

  useEffect(() => {
    if (!auth.currentUser) {
      dispatch(
        loginThunk({
          email: process.env.REACT_APP_STUDENT_EMAIL,
          password: process.env.REACT_APP_STUDENT_PASSWORD
        })
      ).catch(err => {
        alert(err)
        history.push('/auth')
      })
    } else {
      //TODO
      //fetch student and save to the store
    }
    // eslint-disable-next-line
  }, [])

  return (
    <>
      <DrawerContainer
        currentLocation={currentPage}
        handleClick={value => setPage(value)}
        role={role}
      >
        <Suspense fallback={<div>loading...</div>}>
          <div className='main'>
            {currentPage === 'Courses' && account.studentId !== '' && (
              <Course
                token={token}
                studentId={account.studentId}
                showWarning={showWarning}
                role='student'
              />
            )}
            {currentPage === 'Bookings' && account.studentId !== '' && (
              <Booking
                token={token}
                role='student'
                studentId={account.studentId}
              />
            )}
            {currentPage === 'Memberships' && account.studentId !== '' && (
              <Membership
                token={token}
                role='student'
                studentId={account.studentId}
                showWarning={showWarning}
                showSuccess={showSuccess}
                membership={membership}
              />
            )}
            {currentPage === 'Subscriptions' && account.studentId !== '' && (
              <Subscription
                token={token}
                role='student'
                studentId={account.studentId}
              />
            )}
            {open && (
              <Feedback
                open={open}
                message={message}
                severity={severity}
                handleClose={closeFeedback}
              />
            )}
          </div>
        </Suspense>
      </DrawerContainer>
    </>
  )
}

export default Student
