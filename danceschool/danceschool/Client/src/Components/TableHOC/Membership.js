import React, { Suspense } from 'react'
import { useDispatch } from 'react-redux'
import { Button } from '@material-ui/core'
import { useFetch } from '../Hooks/useFetch'
import {
  fetchMemberships,
  fetchCreateMembership,
  fetchUpdateMembership,
  fetchDeleteMembership
} from '../API/Membership'
import { subscribe } from '../../Actions/user.actions'
import { membershipHeader } from '../Table/TableHeaders'
const Table = React.lazy(() => import('../Table/Table'))
const MembershipBody = React.lazy(() => import('../Table/MembershipBody'))
const MembershipForm = React.lazy(() => import('../Forms/Membership'))

const durationOptions = [
  { label: 'weekly', id: 'weekly' },
  { label: 'monthly', value: 'monthly' },
  { label: 'quarterly', value: 'quarterly' },
  { label: 'annually', value: 'annually' }
]

export default function Membership ({
  token = '',
  role = '',
  showWarning,
  showSuccess,
  studentId = '',
  membership = ''
}) {
  const dispatch = useDispatch()

  const [
    data,
    item,
    view,
    setView,
    handleEdit,
    create,
    update,
    deleteItem
  ] = useFetch(
    token,
    role,
    fetchMemberships,
    fetchCreateMembership,
    fetchUpdateMembership,
    fetchDeleteMembership,
    showSuccess,
    showWarning
  )

  const handleSubscribe = async membershipToSubscribe => {
    if (membership && membership !== '') {
      showWarning(
        'You already have a membership. Please see your subscriptions.'
      )
      return
    }
    const response = await dispatch(subscribe(membershipToSubscribe, studentId, token))
    console.log("succeeded", response)
    if(response){
      showSuccess("You have subscribed to the membership!")
    }
    else{
      showWarning(
        response.error
      )
    }
  }

  return (
    <Suspense fallback={<div>loading...</div>}>
      <>
        {view === 'main' && (
          <>
            {role === 'admin' && (
              <Button
                disabled={token === ''}
                className='navbar-create-btn'
                onClick={() => setView('create')}
              >
                Create Membership
              </Button>
            )}
            <Table tableHeader={membershipHeader}>
              <MembershipBody
                memberships={data}
                handleEdit={handleEdit}
                handleDelete={deleteItem}
                role={role}
                handleSubscribe={handleSubscribe}
              />
            </Table>
          </>
        )}
        {view === 'create' && (
          <MembershipForm
            onContinue={create}
            onCancel={() => setView('main')}
            durationOptions={durationOptions}
            buttonLabel='Create'
          />
        )}
        {view === 'update' && item && (
          <MembershipForm
            item={item}
            onContinue={update}
            onCancel={() => setView('main')}
            durationOptions={durationOptions}
            buttonLabel='Update'
          />
        )}
      </>
    </Suspense>
  )
}
