import React, { Suspense } from 'react'
import {
  fetchStudents,
  unRegisterStudent,
  fetchSearchStudentByName
} from '../API/Student'
import { studentHeader } from '../Table/TableHeaders'
import { useSearch } from '../Hooks/useSearch'
import Pagination from '@material-ui/lab/Pagination'
import { useFetchPagination } from '../Hooks/useFetchPagination'
import SearchBar from 'material-ui-search-bar'

const Table = React.lazy(() => import('../Table/Table'))
const StudentBody = React.lazy(() => import('../Table/StudentBody'))

export default function Student ({
  count = 0,
  token = '',
  role = '',
  showWarning,
  showSuccess
}) {
  const { data, deleteItem, handleChangePage } = useFetchPagination(
    token,
    role,
    fetchStudents,
    () => {},
    () => {},
    unRegisterStudent,
    showWarning,
    showSuccess
  )

  const [state, setState, handleSearch] = useSearch(
    fetchSearchStudentByName,
    token
  )

  return (
    <Suspense fallback={<div>loading...</div>}>
      <SearchBar
        value={state.value}
        onChange={newValue => setState({ value: newValue })}
        onRequestSearch={handleSearch}
      />
      <Table tableHeader={studentHeader}>
        <StudentBody
          students={!state.data ? data.values[data.page] : state.data}
          handleDelete={deleteItem}
        />
      </Table>
      {!state.data && (
        <Pagination
          count={Math.ceil(count / 10)}
          page={data.page}
          onChange={handleChangePage}
          shape='rounded'
        />
      )}
    </Suspense>
  )
}
