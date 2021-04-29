import React, { Suspense } from 'react'
import { Button } from '@material-ui/core'
import { useInstructorCRUD } from '../Hooks/useInstructorCRUD'
import { fetchSearchInstructorByName } from '../API/Instructor'
import { useSearch } from '../Hooks/useSearch'
import { instructorHeader } from '../Table/TableHeaders'
import SearchBar from 'material-ui-search-bar'

const Table = React.lazy(() => import('../Table/Table'))
const InstructorBody = React.lazy(() => import('../Table/InstructorBody'))
const InstructorForm = React.lazy(() => import('../Forms/Instructor'))

export default function Instructor ({
  role = '',
  token = '',
  showWarning,
  showSuccess
}) {
  const [
    view,
    setView,
    item,
    instructors,
    handleEdit,
    create,
    update,
    deleteItem
  ] = useInstructorCRUD(showSuccess, showWarning)

  const [state, setState, handleSearch] = useSearch(
    fetchSearchInstructorByName,
    token
  )

  console.log('state', state)

  return (
    <Suspense fallback={<div>loading...</div>}>
      <>
        {view === 'main' && (
          <>
            <Button
              className='navbar-create-btn'
              onClick={() => setView('create')}
            >
              Create
            </Button>
            <SearchBar
              value={state.value}
              onChange={newValue => setState({ value: newValue })}
              onRequestSearch={handleSearch}
            />
            <Table tableHeader={instructorHeader}>
              <InstructorBody
                instructors={!state.data ? instructors : state.data}
                handleEdit={handleEdit}
                handleDelete={deleteItem}
              />
            </Table>
          </>
        )}
        {view === 'create' && (
          <InstructorForm
            onContinue={create}
            onCancel={() => setView('main')}
            buttonLabel='Create'
          />
        )}
        {view === 'update' && item !== undefined && (
          <InstructorForm
            item={item}
            onContinue={update}
            onCancel={() => setView('main')}
            buttonLabel='Update'
          />
        )}
      </>
    </Suspense>
  )
}
