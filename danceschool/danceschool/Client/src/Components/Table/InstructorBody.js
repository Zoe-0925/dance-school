import React from 'react'
import { TableRow } from '@material-ui/core'
import {
  TableEditIconButton,
  TableDeleteIconButton
} from '../Buttons/IconButtons'
import TableCell from './TableCell'
import { v4 as uuidv4 } from 'uuid'

const InstructorTableBody = ({
  instructors = [],
  handleEdit,
  handleDelete
}) => (
  <>
    {instructors.length > 0 ? (
      instructors.map(instructor => (
        <TableRow key={instructor.id} className='table-body'>
          {[instructor.firstName, instructor.lastName, instructor.email].map(
            each => (
              <TableCell key={uuidv4()} label={each} />
            )
          )}
          <TableEditIconButton onClick={() => handleEdit(instructor)} />
          <TableDeleteIconButton
            onClick={() => {
              handleDelete(instructor.id)
            }}
          />
        </TableRow>
      ))
    ) : (
      <TableRow></TableRow>
    )}
  </>
)

export default InstructorTableBody
