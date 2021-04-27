import React from 'react'
import { TableRow, Button } from '@material-ui/core'
import {
  TableEditIconButton,
  TableDeleteIconButton
} from '../Buttons/IconButtons'
import TableCell from './TableCell'
import { v4 as uuidv4 } from 'uuid'
import { useButtonStyles } from '../Util'

const MembershipTableBody = ({
  memberships = [],
  role = '',
  handleEdit,
  handleDelete,
  handleSubscribe
}) => {
  const classes = useButtonStyles()

  return (
    <>
      {memberships.length > 0 ? (
        memberships.map(membership => (
          <TableRow key={membership.id} className='table-body'>
            {[membership.name, membership.duration, membership.price].map(
              each => (
                <TableCell key={uuidv4()} label={each} />
              )
            )}
            {role === 'admin' && (
              <>
                <TableEditIconButton onClick={() => handleEdit(membership)} />
                <TableDeleteIconButton
                  onClick={() => handleDelete(membership.id)}
                />
              </>
            )}
            {role === 'student' && (
              <td>
                <Button
                  classes={{
                    root: classes.root, // class name, e.g. `classes-nesting-root-x`
                    label: classes.label // class name, e.g. `classes-nesting-label-x`
                  }}
                  className='navbar-create-btn'
                  onClick={() => handleSubscribe(membership)}
                >
                  Subscribe
                </Button>
              </td>
            )}
          </TableRow>
        ))
      ) : (
        <TableRow></TableRow>
      )}
    </>
  )
}

export default MembershipTableBody
