import React from 'react'
import Card from '@material-ui/core/Card'
import { Col } from 'reactstrap'
import { v4 as uuidv4 } from 'uuid'

const MyCard = ({
  item,
  handleClick,
  img,
  name = '',
  second = '',
  third = '',
  price = '',
}) => (
  <Card
    className='card-container'
    key={uuidv4()}
    onClick={() =>  handleClick(item)}
  >
    {img && <img className='card-image' src={img.src} alt={img.alt} />}
    <Col>
      <p className='card-name'>{name}</p>
      {second !== '' && <p className='card-line'>{second}</p>}
      {third !== '' && <p className='card-line'>{third}</p>}
      {price !== '' && <p className='card-price'>Price: {price} AUD</p>}
    </Col>
  </Card>
)

export default MyCard
