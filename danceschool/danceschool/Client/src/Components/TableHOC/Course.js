import React, { Suspense } from 'react'
import {
  fetchCourses,
  fetchCreateCourse,
  fetchDeleteCourse,
  fetchUpdateCourse
} from '../API/Course'
import { useFetchPagination } from '../Hooks/useFetchPagination'
import { useInstructor } from '../Hooks/useInstructor'
import CourseList from '../List/CourseList'

const CourseForm = React.lazy(() => import('../Forms/Course'))
const DanceClass = React.lazy(() => import('./DanceClass'))

export default function Course ({
  token = '',
  role = '',
  showWarning,
  showSuccess,
  count = 0
}) {
  const { instructorOptions } = useInstructor(token)

  const {
    data,
    item,
    setItem,
    view,
    setView,
    create,
    update,
    deleteItem,
    handleChangePage
  } = useFetchPagination(
    token,
    role,
    fetchCourses,
    fetchCreateCourse,
    fetchUpdateCourse,
    fetchDeleteCourse,
    showWarning,
    showSuccess
  )

  const handleClick = id => {
    if (data.values[data.page] && data.values[data.page].length > 0) {
      const courseOfThisPage = data.values[data.page]
      let targetCourse = courseOfThisPage.find(d => d.id === id)
      setItem(targetCourse)
      setView('classes')
    }
  }

  return (
    <>
      {view === 'main' && (
        <>
          <CourseList
            token={token}
            count={count}
            page={data.page}
            data={
              data.values && data.values[data.page]
                ? data.values[data.page]
                : []
            }
            updateCourse={course => {
              setItem(course)
              setView('updateCourse')
            }}
            deleteCourse={deleteItem}
            createCourse={() => setView('createCourse')}
            handleClick={handleClick}
            createDanceClass={() => setView('createDanceClass')}
            handleChangeCoursePage={handleChangePage}
          />
        </>
      )}
      <Suspense fallback={<div>loading...</div>}>
        {(view === 'createCourse' || (view === 'updateCourse' && item)) && (
          <CourseForm
            view={view}
            setMainView={() => setView('main')}
            item={item}
            instructorOptions={instructorOptions}
            createCourse={create}
            updateCourse={update}
          />
        )}
        {view === 'classes' && (
          <DanceClass
            course={item}
            goBack={() => setView('main')}
            token={token}
            setItem={setItem}
            setView={setView}
            view={view}
            showWarning={showWarning}
            showSuccess={showSuccess}
            role={role}
          />
        )}
      </Suspense>
    </>
  )
}
