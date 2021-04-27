import { useState, useEffect } from 'react'

export const useFetchPagination = (
  token,
  role,
  fetchCallback,
  creatCallBack,
  updateCallback,
  deleteCallback,
  showWarning,
  showSuccess,
  studentId = ''
) => {
  const [data, setData] = useState({ page: 1, values: {}, count: 0 })
  const [view, setView] = useState('main')
  const [item, setItem] = useState()

  async function fetchUrl () {
    if (data.values[data.page] === undefined) {
      const response = await fetchCallback(1, token, studentId)
      setData({
        ...data,
        count: response.count,
        values: { [data.page]: response.data }
      })
    }
  }

  useEffect(() => {
    fetchUrl()
    // eslint-disable-next-line
  }, [])

  const create = async value => {
    if (token !== '' && role === 'admin') {
      const response = await creatCallBack(value, token, studentId)
      if (response.data) {
        setData([...data, { ...value, id: response.data }])
        showSuccess('Successfully created!')
      } else {
        showWarning(response.error.message)
      }
      setView('main')
    }
  }

  const update = async value => {
    if (token !== '' && role === 'admin') {
      const valueWithId = { ...value, id: item.id }
      const response = await updateCallback(valueWithId, token)
      if (response.data) {
        let copy = [...data]
        let target = copy.find(m => m.id === item.id)
        copy[copy.indexOf(target)] = valueWithId
        setData(copy)
        setView('main')
        showSuccess('Successfully updated!')
      } else {
        showWarning(response.error.message)
      }
    }
  }

  const deleteItem = async id => {
    if (token !== '' && role === 'admin') {
      const response = await deleteCallback(id, token, studentId)
      if (response.data) {
        let valuesCopy = [...data.values]
        valuesCopy[data.page].filter(item => item.id !== id)
        setData({ ...data, values: valuesCopy })
        showSuccess('Successfully deleted!')
      } else {
        showWarning(response.error.message)
      }
    }
  }

  const handleChangePage = async (e, page) => {
    const aPage = await fetchCallback(page, token, studentId)
    setData({
      ...data,
      page: page,
      values: { ...data.values, [page]: aPage.data }
    })
  }

  return {
    data,
    item,
    setItem,
    view,
    setView,
    create,
    update,
    deleteItem,
    handleChangePage
  }
}
