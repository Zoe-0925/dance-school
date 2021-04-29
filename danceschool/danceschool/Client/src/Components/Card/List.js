import React from 'react'
import Card from '@material-ui/core/Card'
import { v4 as uuidv4 } from 'uuid'

export const List = ({ className, title, line1, line2, line3 }) => (
  <Card align='center' className={className} key={uuidv4()}>
    <p className='visualization-card-body'>{title}</p>
    <br />
    <p className='visualization-card-body'> {line1}</p>
    <br />
    <p className='visualization-card-body'>{line2}</p>
    <br />
    <p className='visualization-card-body'> {line3}</p>
  </Card>
)
