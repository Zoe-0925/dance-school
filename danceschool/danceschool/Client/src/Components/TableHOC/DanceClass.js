import React from 'react'
import Pagination from '@material-ui/lab/Pagination'
import DanceClassTableBody from '../Table/DanceClassBody'
import { Button } from '@material-ui/core'
import { useDanceClassCRUD } from '../Hooks/useDanceClassCRUD'
import { Row } from 'reactstrap'
const DanceClassForm = React.lazy(() => import('../Forms/DanceClass'))

export default function DanceClass ({
  course,
  goBack,
  token = '',
  role = '',
  showWarning,
  showSuccess
}) {
  const {
    data,
    view,
    setView,
    item,
    setItem,
    createDanceClass,
    updateDanceClass,
    deleteDanceClass,
    getClasses
  } = useDanceClassCRUD(course, token, showWarning, showSuccess, role)

  const classList =
    course && data && data[course.id] && data[course.id].danceClasses
      ? data[course.id]
      : []

  return (
    <>
      {view === 'classes' && data && data[course.id] && (
        <>
          <Row>
            {role === 'admin' && (
              <Button
                className='navbar-create-btn'
                onClick={() => setView('createDanceClass')}
              >
                Create Dance Class
              </Button>
            )}
            <Button className='cancel-btn' onClick={goBack}>
              Back to the courses
            </Button>
          </Row>
          {classList && (
            <DanceClassTableBody
              role={role}
              danceClasses={classList.danceClasses[classList.page]}
              course={course}
              handleEdit={danceClass => {
                setItem(danceClass)
                setView('updateDanceClass')
              }}
              handleDelete={value => deleteDanceClass(value, token)}
              openBooking={danceClass => {
                setView('booking')
                setItem(danceClass)
              }}
            />
          )}
          <Pagination
            count={Math.ceil(classList.count / 10)}
            page={classList.page}
            onChange={async (e, page) => {
              await getClasses(course.id, page, course.classCount)
            }}
            shape='rounded'
          />
          <Button className='navbar-create-btn' onClick={goBack}>
            Back to the courses
          </Button>
        </>
      )}
      {(view === 'createDanceClass' ||
        (view === 'updateDanceClass' && item)) && (
        <DanceClassForm
          view={view}
          setMainView={() => setView('classes')}
          item={item}
          createDanceClass={value =>
            createDanceClass({ ...value, courseId: course.id }, token)
          }
          updateDanceClass={value => updateDanceClass(value, token)}
        />
      )}
    </>
  )
}
