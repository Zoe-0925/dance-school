import React, { useState, useEffect, Suspense } from 'react'
import {
  fetchSubscriptions,
  fetchCancelSubscription,
  fetchStudentSubscriptions
} from '../API/Subscription'
import { subscriptionHeader } from '../Table/TableHeaders'
import Pagination from '@material-ui/lab/Pagination'
const Table = React.lazy(() => import('../Table/Table'))
const SubscriptionBody = React.lazy(() => import('../Table/SubscriptionBody'))

export default function Subscription ({
  token = '',
  role = '',
  studentId = '',
  count = 0
}) {
  const [subscriptions, setSubscriptions] = useState([])
  const [data, setData] = useState({ page: 1, subscriptions: {} })

  useEffect(() => {
    if (role === 'admin') {
      fetchSubscriptions(1, token).then(result => {
        setSubscriptions(result)
        setData({ ...data, subscriptions: { [data.page]: result } })
      })
    }
    if (role === 'student') {
      fetchStudentSubscriptions(studentId, 1, token).then(result => {
        setSubscriptions(result)
        setData({ ...data, subscriptions: { [data.page]: result } })
      })
    }
    // eslint-disable-next-line
  }, [token])

  const deleteItem = id => {
    fetchCancelSubscription(id, token)
    const updated = [...subscriptions].filter(item => item.id !== id)
    setSubscriptions(updated)
  }

  const handleChangePage = async (e, page) => {
    if (role === 'admin') {
      const aPageOfSubscriptions = await fetchSubscriptions(page, token)
      setData({
        page: page,
        students: { ...data.subscriptions, [page]: aPageOfSubscriptions }
      })
    }
  }

  return (
    <Suspense fallback={<div>loading...</div>}>
      <Table tableHeader={subscriptionHeader}>
        {data.subscriptions[data.page] && (
          <SubscriptionBody
            subscriptions={subscriptions}
            handlecancel={deleteItem}
          />
        )}
      </Table>
      <Pagination
        count={Math.ceil(count / 10)}
        page={data.page}
        onChange={handleChangePage}
        shape='rounded'
      />
    </Suspense>
  )
}
