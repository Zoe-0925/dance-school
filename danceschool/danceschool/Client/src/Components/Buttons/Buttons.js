import React from 'react'
import { Button } from '@material-ui/core'
import { TableCell } from '@material-ui/core'
import { v4 as uuidv4 } from 'uuid'

export const EditButton = ({ onClick, }) => (
  <Button className='navbar-create-btn' onClick={onClick}>
    Edit
  </Button>
)

export const DeleteButton = ({ onClick }) => (
  <Button className='cancel-btn' onClick={onClick}>
    Delete
  </Button>
)

export const TableEditButton = ({ onClick }) => (
  <TableCell key={uuidv4()} align='center'>
    <EditButton onClick={onClick} />
  </TableCell>
)

export const TableDeleteButton = ({ onClick }) => (
  <TableCell key={uuidv4()} align='center'>
    <DeleteButton onClick={onClick} />
  </TableCell>
)
