import React from 'react'
import { TableRow } from '@material-ui/core'
import { TableDeleteIconButton } from '../Buttons/IconButtons'
import TableCell from './TableCell'
import { v4 as uuidv4 } from 'uuid'


const StudentTableBody = ({ students = [], handleDelete }) => (
  <>
    {students.length > 0 ? (
      students.map(student => (
        <TableRow key={student.id} className='table-body'>
          {[
            student.userName,
            student.email,
            student.subscription && student.subscription[0].membership.Name
          ].map(each => (
            <TableCell label={each}  key={uuidv4()}/>
          ))}
          <TableDeleteIconButton  onClick={() => handleDelete(student.id)} />
        </TableRow>
      ))
    ) : (
      <TableRow></TableRow>
    )}
  </>
)

export default StudentTableBody
