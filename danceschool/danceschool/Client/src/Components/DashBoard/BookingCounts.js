import React, { useState, useEffect } from 'react'
import BarChart from '../Charts/BarChart'
import { formatPieChartData } from '../Util'
import { fetchBookingCount } from '../API/Analytics'

const BookingCounts = ({ token = '', data, label = 'This week' }) => {
  const [state, setState] = useState()
  const [display, setDisplay] = useState()
  const [open, setOpen] = useState(false)
  const anchorRef = React.useRef(null)

  useEffect(() => {
    if (data) {
      const weekData = formatPieChartData(data, label)
      setState({ week: weekData })
      setDisplay(weekData)
    }
  }, [data, label])

  const handleChange = type => {
    if (state[type]===undefined) {
      //If not cached yet, fetch from API.
      fetchBookingCount(type, token).then(result => {
        const formatted = formatPieChartData(result, 'This ' + type)
        setDisplay(formatted)
        setState({ ...state, [type]: formatted })
      })
    } else {
      setDisplay(state[type])
    }
    setOpen(false)
  }

  return (
    <BarChart
      data={display}
      handleOpen={() => setOpen(true)}
      open={open}
      handleChange={handleChange}
      handleClose={() => setOpen(false)}
      anchorRef={anchorRef}
    />
  )
}

export default BookingCounts
