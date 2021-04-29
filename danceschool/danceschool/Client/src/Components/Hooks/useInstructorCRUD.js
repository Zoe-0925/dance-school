import { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { selectInstructors } from '../../Reducers/selectors'
import {
  fetchInstructors,
  registerInstructor,
  fetchUpdateInstructor,
  unRegisterInstructor
} from '../API/Instructor'
import {
  appendInstructors,
  ADD_INSTRUCTOR,
  UPDATE_INSTRUCTOR,
  DELETE_INSTRUCTOR
} from '../../Actions/instructor.actions'

export const useInstructorCRUD = (token = '', showWarning, showSuccess) => {
  const dispatch = useDispatch()
  const [view, setView] = useState('main')
  const [item, setItem] = useState()

  const handleEdit = data => {
    setItem(data)
    setView('update')
  }

  const instructors = useSelector(selectInstructors)

  useEffect(() => {
    if (instructors.length === 0 && token !== '') {
      fetchInstructors().then(result => {
        dispatch(appendInstructors(result))
      })
    }
    // eslint-disable-next-line
  }, [])

  const create = async data => {
    if (token !== '') {
      const response = await registerInstructor(data, token)
      if (response.data) {
        dispatch({
          type: ADD_INSTRUCTOR,
          data: [...instructors, { ...data, id: response.data }]
        })
      } else {
        showWarning(response.error.message)
      }
      setView('main')
    }
  }

  const update = async data => {
    if (token !== '') {
      const dataWithId = { ...data, id: item.id }
      const response = await fetchUpdateInstructor(dataWithId, token)
      if (response.data) {
        dispatch({ type: UPDATE_INSTRUCTOR, data: dataWithId })
        setView('main')
      } else {
        showWarning(response.error.message)
      }
    }
  }

  const deleteItem = async id => {
    const response = await unRegisterInstructor(id, token)
    if (response.data) {
      dispatch({ type: DELETE_INSTRUCTOR, id: id })
    } else {
      showWarning(response.error.message)
    }
  }

  return [view, setView, item ,instructors, handleEdit, create, update, deleteItem]
}
