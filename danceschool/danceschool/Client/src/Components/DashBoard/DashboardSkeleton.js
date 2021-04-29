import React from 'react'
import { Row, Col } from 'reactstrap'
import Skeleton from '@material-ui/lab/Skeleton'

const DashboardSkeleton = () => {
  const analyticalCardSkeletons = () => {
    for (let i = 0; i < 4; i++) {
      return (
        <Skeleton
          className='analytics-card-container'
          variant='rect'
          animation='wave'
        />
      )
    }
  }

  return (
    <div className='card-list'>
      {analyticalCardSkeletons}
      <Row className='full-width'>
        <Col xs={12} sm={8}>
          <Skeleton
            width="100%"
            height="100%"
            variant='rect'
            animation='wave'
          />
        </Col>
        <Col xs={12} sm={4}>
          <Skeleton
            width="100%"
            height="100%"
            variant='rect'
            animation='wave'
          />
        </Col>
      </Row>
    </div>
  )
}

export default DashboardSkeleton
