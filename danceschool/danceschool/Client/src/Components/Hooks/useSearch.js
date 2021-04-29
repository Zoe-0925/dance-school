import { useState } from 'react'

export const useSearch = (search, token = '') => {
  const [state, setState] = useState({ value: '' })

  const handleSearch = async () => {
    const response = await search(state.value, token)
    setState({
      ...state,
      data: response
    })
  }


  return [state, setState, handleSearch]
}
