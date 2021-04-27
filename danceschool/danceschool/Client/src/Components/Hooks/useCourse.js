import { useState, useEffect } from 'react'

export const useCourse = (token, fetchCallback, studentId = '') => {
  const [data, setData] = useState({ page: 1, values: [] })
  const [view, setView] = useState('main')
  const [item, setItem] = useState()
  const [page, setPage] = useState(1)

  async function fetchUrl () {
    const response = await fetchCallback(1, token, studentId)
    console.log("reponse", response)
    setData({
      ...data,
      count: response.count,
      values: { [data.page]: response.data }
    })
  }

  const handleChangePage = async (e, page) => {
    const aPage = await fetchCallback(page, token, studentId)
    setData({
      ...data,
      page: page,
      values: { ...data.values, [page]: aPage.data }
    })
  }

  useEffect(() => {
    fetchUrl()
    // eslint-disable-next-line
  }, [])

  return {
    data,
    item,
    setItem,
    view,
    setView,
    handleChangePage
  }
}
