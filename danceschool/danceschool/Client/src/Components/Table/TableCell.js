import React from 'react'
import { TableCell } from '@material-ui/core'
import { v4 as uuidv4 } from 'uuid'

const MyTableCell = ({ label = '', className }) => (
  <TableCell
    className={className ? className : 'table-cell'}
    key={uuidv4()}
    align='left'
  >
    {label}
  </TableCell>
)

export default MyTableCell
