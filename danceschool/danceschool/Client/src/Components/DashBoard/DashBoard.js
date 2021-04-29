import React, { Suspense } from 'react'
import { Row, Col } from 'reactstrap'
import { v4 as uuidv4 } from 'uuid'
import { useDashboard } from '../Hooks/useDashboard'
import DashboardCard from '../Card/DashboardCard'
import DashboardSkeleton from "./DashboardSkeleton"
import GroupIcon from '@material-ui/icons/Group'
import { makeStyles } from '@material-ui/core/styles'
import { deepOrange, deepPurple } from '@material-ui/core/colors'
const BookingByMembership = React.lazy(() => import('./BookingByMembership'))
const BookingCounts = React.lazy(() => import('./BookingCounts'))

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
    '& > *': {
      margin: theme.spacing(1)
    }
  },
  orange: {
    color: theme.palette.getContrastText(deepOrange[500]),
    backgroundColor: deepOrange[500]
  },
  purple: {
    color: theme.palette.getContrastText(deepPurple[500]),
    backgroundColor: deepPurple[500]
  },
  avatar: {
    width: theme.spacing(7),
    height: theme.spacing(7)
  }
}))

const Line = ({ content }) => (
  <Row>
    <Col xs={8}>{content.name}</Col>
    <Col xs={4}>{content.count}</Col>
  </Row>
)

const DashBoard = ({ token = '', role = '', handleCount }) => {
  const { data, loading } = useDashboard(token, role, handleCount)
  const classes = useStyles()

  return (
    <div>
    {loading && <DashboardSkeleton/>}
      {!loading && data && (
        <>
          <div className='card-list'>
            <DashboardCard
              title='Top Classes by booking'
              content={data.topClasses.map(each => (
                <Line key={uuidv4()} content={each} />
              ))}
              style={classes.orange + ' ' + classes.avatar}
            />
            <DashboardCard
              title='Total Students'
              figure={data.totalStudents}
              style={classes.orange + ' ' + classes.avatar}
              icon={<GroupIcon />}
            />
            <DashboardCard
              title='Total Bookings'
              figure={data.totalBookings}
              style={classes.purple + ' ' + classes.avatar}
              icon={<GroupIcon />}
            />
            {data.topInstructors.length > 0 && (
              <DashboardCard
                title='Top Instructors by booking'
                content={data.topInstructors.map(each => (
                  <Line key={uuidv4()} content={each} />
                ))}
                style={classes.orange + ' ' + classes.avatar}
              />
            )}
          </div>
          <Suspense fallback={<div>loading...</div>}>
            <Row className='full-width'>
              <Col xs={12} sm={8}>
                {data && data.lastWeekbookings && (
                  <BookingCounts data={data.lastWeekbookings} token={token} />
                )}
              </Col>
              <Col xs={12} sm={4}>
                {data && data.bookingByMembership && (
                  <BookingByMembership
                    data={data.bookingByMembership}
                    totalBookings={data.totalBookings}
                  />
                )}
              </Col>
            </Row>
          </Suspense>
        </>
      )}
    </div>
  )
}

export default DashBoard

//
