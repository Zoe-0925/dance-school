import React, { useState, useEffect } from 'react'
import { fetchSubscribe } from '../API/Subscription'
import Card from '../Card/Card'
import { formatISO, add } from 'date-fns'
import { convertDuration } from '../Util'

const Membership = ({ data, studentId, token, handleSubscription }) => {
  const subscribe = async membership => {
    const subscription = {
      studentId: studentId,
      membershipId: membership.id,
      date: formatISO(new Date(), { representation: 'date' }),
      nextBillingDate: formatISO(
        add(new Date(), convertDuration(membership.duration))
      ),
      canceled: false
    }
    const subscriptionId = await fetchSubscribe(subscription, token)
    handleSubscription({ ...subscription, id: subscriptionId })
  }

  return (
    <div className='card-list'>
      {data.map(each => (
        <Card
          handleClick={() => subscribe(each)}
          img={{ src: each.img || '', alt: each.name }}
          name={each.name}
          second={each.duration}
          third=''
          price={each.price}
          buttonLabel='Save $$'
        />
      ))}
    </div>
  )
}

export default Membership
