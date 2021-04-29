import React, { useState, useEffect } from 'react'
import Card from '../Card/Card'
import { getWeekDayfromDate } from '../Util'
import BookingForm from '../Forms/Booking'
import { formatISO } from 'date-fns'
import { fetchCreateBooking } from '../API/Booking'

const DanceClass = ({ data, studentId, token, click }) => {
 
  const book = async bookingWithMultipleDates => {
    const today = new Date()
    bookingWithMultipleDates.date.foreach(async each => {
      await fetchCreateBooking(
        {
          ...bookingWithMultipleDates,
          date: formatISO(each, { representation: 'date' }),
          bookingDate: formatISO(today, { representation: 'date' })
        },
        token
      )
    })
    //TODO
    //Successful feedback. => booking confirmation
  }

  return (
    <div className='card-list'>
        {data.map(each => (
          <Card
            handleClick={() => click(each)}
            name={each.name}
            second={'6-week courses from ' + getWeekDayfromDate(each.startDate)}
            third={each.startHour + ':00 - ' + each.endHour + ':00'}
            price={each.price}
            buttonLabel='Book now'
          />
        ))}
    </div>
  )
}

export default DanceClass
