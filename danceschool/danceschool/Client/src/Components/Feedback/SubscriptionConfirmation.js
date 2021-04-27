import React from 'react'
import { formatDate } from '../Util'
import { Row } from 'reactstrap'

const SubscriptionConfirmation = ({ subscription, membership }) => (
  <div>
    <Row align='center'>
      <p>Thanks for joining our membership!</p>
    </Row>
    <Row align='center'>
      {membership.name + ' membership (' + membership.duration + ')'}
    </Row>
    <div className='form'>
      <Row>
        <p>Start Date: {formatDate(subscription.date)}</p>
      </Row>
      <Row>
        <p>Next Billing Date: {formatDate(subscription.nextBillingDate)}</p>
      </Row>
      <Row>
        <p>Price: {membership.price}</p>
      </Row>
    </div>
  </div>
)

export default SubscriptionConfirmation
