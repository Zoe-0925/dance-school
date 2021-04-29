import React from 'react'
import { getWeekDayfromDate, formatDate } from '../Util'
import {Row, Col} from "reactstrap"

const BookingConfirmation = ({ danceClass, bookings = [] }) => (
  <div>
    <Row align='center'>
      <p>Thanks for booking!</p>
    </Row>
    <Row align='center'>
      <p>{bookings.length > 0 && bookings[0].studentName}</p>
    </Row>
    <Row>{danceClass.name}</Row>
    <Row>
      {getWeekDayfromDate(danceClass.startDate) +
        ' ' +
        danceClass.startTime +
        ':00-' +
        danceClass.endTime}
    </Row>
    <Row>
      <Col xs={2}>
        <p>Booked at:</p>
      </Col>
      <Col xs={10}>
        {bookings.map(each => (
          <Row> {formatDate(each.date)}</Row>
        ))}
      </Col>
    </Row>
  </div>
)

export default BookingConfirmation
