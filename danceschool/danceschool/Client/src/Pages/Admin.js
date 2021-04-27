import React, { useState, useEffect, Suspense } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useFeedback } from '../Components/Hooks/useFeedback'
import { selectUserReducer } from '../Reducers/selectors'
import DrawerContainer from '../Components/Drawer/DrawerContainer'
import { loginThunk } from '../Actions/user.actions'
import { auth } from '../firebase'
import history from '../history'

const Course = React.lazy(() => import('../Components/TableHOC/Course'))
const Analytics = React.lazy(() => import('../Components/DashBoard/DashBoard'))
const Student = React.lazy(() => import('../Components/TableHOC/Student'))
const Membership = React.lazy(() => import('../Components/TableHOC/Membership'))
const Instructor = React.lazy(() => import('../Components/TableHOC/Instructor'))
const Booking = React.lazy(() => import('../Components/TableHOC/Booking'))
const Feedback = React.lazy(() => import('../Components/Feedback/Feedback'))

const Admin = () => {
  const dispatch = useDispatch()
  const [currentPage, setPage] = useState('Analytics')
  const [counts, setCounts] = useState()
 
  const account = useSelector(selectUserReducer)
  const token = account.token
  const role = account.role

  useEffect(() => {
    if (!auth.currentUser) {
      dispatch(
        loginThunk({
          email: process.env.REACT_APP_ADMIN_EMAIL,
          password: process.env.REACT_APP_ADMIN_PASSWORD
        })
      ).catch(err => {
        alert(err)
        history.push('/auth')
      })
    }
    // eslint-disable-next-line
  }, [])

  /** 
  useEffect(() => {
    if (role && role !== 'admin') {
      dispatch(logOut)
      history.push('/auth')
    }
    // eslint-disable-next-line
  }, [role])*/

  const handleDrawerClick = value => setPage(value)

  const {
    open,
    message,
    severity,
    showWarning,
    showSuccess,
    closeFeedback
  } = useFeedback()

  return (
    <DrawerContainer
      currentLocation={currentPage}
      handleClick={handleDrawerClick}
      role={role}
    >
      <Suspense fallback={<div>loading...</div>}>
        {token !== '' && (
          <div className='main'>
            {currentPage === 'Analytics' && (
              <Analytics
                handleCount={value => setCounts(value)}
                token={token}
                role={role}
              />
            )}
            {currentPage === 'Courses' && (
              <Course
                count={counts.courses}
                token={token}
                role={role}
                showWarning={showWarning}
                showSuccess={showSuccess}
              />
            )}
            {currentPage === 'Memberships' && (
              <Membership
                token={token}
                role={role}
                showWarning={showWarning}
                showSuccess={showSuccess}
              />
            )}
            {currentPage === 'Bookings' && counts && (
              <Booking
                count={counts.bookings}
                token={token}
                role={role}
                showWarning={showWarning}
                showSuccess={showSuccess}
              />
            )}
            {currentPage === 'Students' && counts && (
              <Student
                count={counts.students}
                token={token}
                role={role}
                showWarning={showWarning}
                showSuccess={showSuccess}
              />
            )}
            {currentPage === 'Instructors' && (
              <Instructor
                token={token}
                role={role}
                showWarning={showWarning}
                showSuccess={showSuccess}
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
        )}
      </Suspense>
    </DrawerContainer>
  )
}

export default Admin

/**const Subscription = React.lazy(() =>
  import('../Components/TableHOC/Subscription')
)*/

/**
 *               {currentPage === 'Subscriptions' && (
                <Subscription token={token} role={role} />
              )}
 */
