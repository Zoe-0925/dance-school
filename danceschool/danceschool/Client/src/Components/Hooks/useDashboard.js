import { useState, useEffect } from 'react'
import { fetchAnalytics } from '../API/Analytics'

export const useDashboard = (token = '', role = '', handleCount) => {
  const [data, setData] = useState()
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    if (role === 'admin') {
      setLoading(true)
      fetchAnalytics(token).then(result => {
        setData(result)
        handleCount({
          students: result.totalStudents,
          bookings: result.totalBookings,
          subscriptions: result.totalSubscriptions,
          courses: result.totalCourses
        })
      })
      setLoading(false)
      return
    }
    // eslint-disable-next-line
  }, [role])

  useEffect(() => {
    if (data && data.instructors && data.topInstructors[0].name === undefined) {
      let instructors = [...data.topInstructors].map(each => {
        const instructor = data.instructors.find(
          i => i.id === each.instructorID
        )
        const name = instructor
          ? instructor.firstName + ' ' + instructor.lastName
          : ''
        return { ...each, name: name }
      })
      setData({ ...data, topInstructors: instructors })
    }
    return
    // eslint-disable-next-line
  }, [data])

  return { data, loading }
}
