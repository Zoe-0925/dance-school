import React from 'react'
import { TableRow } from '@material-ui/core'
import { TableDeleteButton } from '../Buttons/Buttons'
import TableCell from './TableCell'

const SubscriptionTableBody = ({
  subscriptions = [],
  handleDelete,
  role = ''
}) => (
  <>
    {role === 'student' && (
      <p>Go to the Membership tab in the left to subscribe and save money!</p>
    )}
    {subscriptions.length > 0 ? (
      subscriptions.map(subscription => (
        <TableRow key={subscription.id} className='table-body'>
          {[
            subscription.membership.name,
            subscription.membership.startDate,
            subscription.membership.nextBillingDate,
            subscription.student.userName,
            subscription.student.email,
            subscription.canceled ? 'Canceled' : ''
          ].map(each => (
            <TableCell label={each} />
          ))}
          <TableDeleteButton onClick={() => handleDelete(subscription.id)} />
        </TableRow>
      ))
    ) : (
      <TableRow></TableRow>
    )}
  </>
)

export default SubscriptionTableBody
