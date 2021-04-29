import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { appendInstructors } from '../../Actions/instructor.actions'
import { selectInstructorOptions } from '../../Reducers/selectors'
import { fetchInstructors } from '../API/Instructor'

export const useInstructor = (token = '') => {
  const dispatch = useDispatch()
  const instructorOptions = useSelector(selectInstructorOptions)

  useEffect(() => {
    if (instructorOptions.length === 0 && token !== '') {
      fetchInstructors().then(result => {
        dispatch(appendInstructors(result))
      })
    }
    // eslint-disable-next-line
  }, [])

  return { instructorOptions }
}
