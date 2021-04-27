import React from 'react'
import PieChart from '../Charts/PieChart'
import { colors } from '@material-ui/core'

const BookingByMembership = ({ data, totalBookings }) => {
  const color = [
    colors.indigo[500],
    colors.red[600],
    colors.orange[600],
    colors.blue[600],
    colors.yellow[600]
  ]

  const display = {
    datasets: [
      {
        data: data.map(each => {
          return parseInt((each.count * 100) / totalBookings)
        }),
        backgroundColor: color,
        borderWidth: 8,
        borderColor: colors.common.white,
        hoverBorderColor: colors.common.white
      }
    ],
    labels: data.map(each => each.membershipName)
  }

  const memberships = display.labels.map(each => {
    const index = display.labels.indexOf(each)
    return {
      title: each,
      value:
        display.datasets && display.datasets[0].data
          ? display.datasets[0].data[index]
          : 0,
      color: color[index]
    }
  })

  return <PieChart data={display} memberships={memberships} />
}

export default BookingByMembership
