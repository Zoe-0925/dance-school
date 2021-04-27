import React, { Suspense } from 'react'
import { useCourse } from '../Hooks/useCourse'
import { useBooking } from '../Hooks/useBooking'
import BookingForm from '../Forms/Booking'
import Card from './Card'
import Pagination from '@material-ui/lab/Pagination'
import SearchBar from 'material-ui-search-bar'
import { useSearch } from '../Hooks/useSearch'
import { fetchSearchCourseByName, fetchCourses } from '../API/Course'

const DanceClass = React.lazy(() => import('../TableHOC/DanceClass'))

const Course = (role = '', token = '', studentId, showWarning) => {
  const { data, item, setItem, view, setView, handleChangePage } = useCourse(
    token,
    fetchCourses,
    (studentId = '')
  )


  console.log('role', role)
  //TODO
  //pass to the dance class data prop
  //and on click => open booking form

  const [state, setState, handleSearch] = useSearch(
    fetchSearchCourseByName,
    token
  )

  const { booking, handleBooking } = useBooking(
    'student',
    token,
    showWarning,
    setView
  )
  //TODO
  //1. put the booking hook here
  //2. put the booking form here
  //3. submit booking.

  return (
    <>
      {view === 'main' && data.values[data.page] && (
        <>
          <SearchBar
            value={state.value}
            onChange={newValue => setState({ value: newValue })}
            onRequestSearch={handleSearch}
            onCancelSearch={() => setState({ value: '' })}
          />
          <div className='card-list'>
            {(!state.data ? data.values[data.page] : state.data).map(each => (
              <Card
                key={each.id}
                name={each.name}
                second={each.InstructorID}
                price={each.price}
                item={each}
                handleClick={value => {
                  setItem(value)
                  setView('classes')
                }}
              />
            ))}
          </div>
          <Pagination
            count={Math.ceil(data.count / 10)}
            page={data.page}
            onChange={handleChangePage}
            shape='rounded'
          />
        </>
      )}
      <Suspense fallback={<div>loading...</div>}>
        {view === 'classes' && (
          <DanceClass
            course={item}
            goBack={() => setView('main')}
            token={token}
            setItem={setItem}
            setView={setView}
            view={view}
            role={role.role}
          />
        )}
        {view === 'booking' && (
          <BookingForm
            onContinue={handleBooking}
            studentId={studentId}
            item={item}
          />
        )}
      </Suspense>
    </>
  )
}

export default Course
