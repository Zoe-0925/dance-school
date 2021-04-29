import React from 'react'
import { TableRow } from '@material-ui/core'
import TableCell from './TableCell'
import { v4 as uuidv4 } from 'uuid'
import { formatDate, getWeekDayfromDate, formatTime } from '../Util'
import {
  TableEditIconButton,
  TableDeleteIconButton
} from '../Buttons/IconButtons'
import Table from './Table'
import { Button } from '@material-ui/core'
import { useButtonStyles } from '../Util'

const DanceClassTableBody = ({
  danceClasses = [],
  course,
  handleEdit,
  handleDelete,
  role = '',
  openBooking
}) => {
  console.log('role', role)
  const classes = useButtonStyles()

  return (
    <Table>
      {danceClasses ? (
        danceClasses.map(danceClass => (
          <TableRow key={danceClass.id} className='project-list-table'>
            {[
              {
                className: 'medium-table-cell',
                label: formatDate(danceClass.startTime)
              },
              {
                className: 'medium-table-cell',
                label:
                  formatTime(danceClass.startTime) +
                  '-' +
                  formatTime(danceClass.endTime)
              },
              {
                className: 'medium-table-cell',
                label: (
                  <div className='blue status-tab short-tab'>
                    {getWeekDayfromDate(danceClass.startTime)}
                  </div>
                )
              },
              {
                className: 'large-table-cell',
                label: (
                  <div
                    className={
                      (role === 'admin' &&
                        course.bookingLimit - danceClass.count < 6) ||
                      (role === 'student' &&
                        course.bookingLimit - danceClass.count > 12)
                        ? 'green status-tab'
                        : role === 'admin' &&
                          course.bookingLimit - danceClass.count > 6 &&
                          course.bookingLimit - danceClass.count < 12
                        ? 'orange status-tab'
                        : 'red status-tab'
                    }
                  >
                    {danceClass.count > 0
                      ? (danceClass.count * 100) / course.bookingLimit +
                        '% booked'
                      : role === 'admin'
                      ? 'No booking'
                      : 'Available'}
                  </div>
                )
              },
              {
                className: 'large-table-cell',
                label: 'Price: ' + course.price + ' AUD'
              }
            ].map(each => (
              <TableCell
                key={uuidv4()}
                label={each.label}
                className={each ? each.className : ''}
              />
            ))}

            {role === 'admin' && (
              <>
                <TableEditIconButton
                  onClick={() =>
                    handleEdit({ ...danceClass, courseName: course.name })
                  }
                />
                <TableDeleteIconButton
                  onClick={() => {
                    handleDelete(danceClass.id)
                  }}
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
                onClick={() =>
                  openBooking({ ...danceClass, courseName: course.name })
                }
              >
                Book now
              </Button>
              </td>
            )}
          </TableRow>
        ))
      ) : (
        <p>empty</p>
      )}
    </Table>
  )
}

export default DanceClassTableBody
