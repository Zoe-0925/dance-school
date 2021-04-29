import Card from '../Card/Card'

const Instructor = ({ data, handleInstructor }) => (
  <div className='card-list'>
    {data.map(each => (
      <Card
        handleClick={() => handleInstructor(each.id)}
        img={{ src: each.img || '', alt: each.name }}
        name={each.firstName + ' ' + each.lastName}
        buttonLabel='View Classes'
      />
    ))}
  </div>
)

export default Instructor
