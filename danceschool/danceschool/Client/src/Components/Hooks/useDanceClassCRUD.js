import { useState, useEffect } from 'react'
import {
  fetchCreateClass,
  fetchUpdateClass,
  fetchDeleteClass,
  fetchClassByCourse,
  fetchUpComingClassByCourse
} from '../API/DanceClass'

export const useDanceClassCRUD = (
  course,
  token,
  showWarning,
  showSuccess,
  role
) => {
  const [data, setData] = useState()
  const [view, setView] = useState('classes')
  const [item, setItem] = useState()

  const getClasses = async (courseId, page = 1, count = 0) => {
    let aPage =
      role === 'admin'
        ? await fetchClassByCourse(courseId, page)
        : await fetchUpComingClassByCourse(courseId, page)
    let classesCopy = { ...data }

    classesCopy[courseId] = {
      page: page,
      count: count
    }

    classesCopy[courseId].danceClasses = {
      ...classesCopy[courseId].danceClasses,
      [page]: aPage
    }
    setData(classesCopy)
  }

  useEffect(() => {
    if (course) {
      getClasses(course.id, 1, course.classCount).then(() => {
        return
      })
    }
    // eslint-disable-next-line
  }, [])

  const createDanceClass = async danceClass => {
    const response = await fetchCreateClass(danceClass, token)
    if (response.data) {
      let newClasses = { ...data }
      newClasses[danceClass.courseId].push({
        ...danceClass,
        id: response.id,
        count: 0
      })
      setData(newClasses)
      showSuccess()
    } else {
      showWarning(response.error.message)
    }
    setView('classes')
  }

  const updateDanceClass = async danceClass => {
    const response = await fetchUpdateClass(danceClass, token)
    if (response.data) {
      let newClasses = { ...data }
      let classList = newClasses[danceClass.courseId]
      let target = classList.find(m => m.id === danceClass.id)
      classList[classList.indexOf(target)] = danceClass
      setData(newClasses)
      showSuccess()
    } else {
      showWarning(response.error.message)
    }
    setView('classes')
  }

  const deleteDanceClass = async (courseId, id) => {
    const response = await fetchDeleteClass(id, token)
    if (response.data) {
      let newClasses = { ...data }
      let classList = newClasses[courseId]
      classList.filter(item => item.id === id)
      setData(newClasses)
      showSuccess()
    } else {
      showWarning(response.error.message)
    }
  }

  return {
    data,
    view,
    setView,
    item,
    setItem,
    createDanceClass,
    updateDanceClass,
    deleteDanceClass,
    getClasses
  }
}
