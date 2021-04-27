import { useState, useEffect } from 'react'

export const useFetch = (
  token,
  role,
  fetchCallback,
  creatCallBack,
  updateCallback,
  deleteCallback,
  showWarning,
  showSuccess
) => {
  const [data, setData] = useState([])
  const [view, setView] = useState('main')
  const [item, setItem] = useState()

  const handleEdit = value => {
    setItem(value)
    setView('update')
  }

  async function fetchUrl () {
    const response = await fetchCallback(token)
    setData(response)
  }

  useEffect(() => {
    fetchUrl()
    // eslint-disable-next-line
  }, [])

  const create = async value => {
    if (token !== '' && role === 'admin') {
      const response = await creatCallBack(value, token)
      if (response.data) {
        setData([...data, { ...value, id: response.data }])
        setView('main')
        showSuccess('Successfully created!')
      } else {
        showWarning(response.error.message)
      }
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
      } else {
        showWarning(response.error.message)
      }
    }
  }

  const deleteItem = async id => {
    if (token !== '' && role === 'admin') {
      const response = await deleteCallback(id, token)
      if (response.data) {
        const updated = [...data].filter(item => item.id !== id)
        setData(updated)
      } else {
        showWarning(response.error.message)
      }
    }
  }

  return [data, item, view, setView, handleEdit, create, update, deleteItem]
}
