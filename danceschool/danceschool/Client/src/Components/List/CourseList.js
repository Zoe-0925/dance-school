import React from 'react'
import { Row, Col } from 'reactstrap'
import DropDownList from './Container'
import DropDownListItem from './Item'
import { v4 as uuidv4 } from 'uuid'
import { Typography, Button } from '@material-ui/core'
import Pagination from '@material-ui/lab/Pagination'
import SearchBar from 'material-ui-search-bar'
import { useSearch } from '../Hooks/useSearch'
import { fetchSearchCourseByName } from '../API/Course'

const CourseList = ({
  data = [],
  count = 0,
  page = 1,
  updateCourse,
  createCourse,
  deleteCourse,
  createDanceClass,
  handleClick,
  handleChangeCoursePage,
  token = ''
}) => {
  const [state, setState, handleSearch] = useSearch(
    fetchSearchCourseByName,
    token
  )
  return (
    <>
      <Row>
        <Col>
          <Button className='navbar-create-btn' onClick={createCourse}>
            Create Course
          </Button>
        </Col>
        <Col xs='auto' align='center'>
          <Typography variant='h5'>
            Click the course name to view classes
          </Typography>
        </Col>
        <Col></Col>
      </Row>
      <SearchBar
        value={state.value}
        onChange={newValue => setState({ value: newValue })}
        onRequestSearch={handleSearch}
        onCancelSearch={() => setState({ value: '' })}
      />
      <DropDownList className='list'>
        {(!state.data ? data : state.data).map(course => (
          <Row key={uuidv4()} style={{ width: '100%' }}>
            <Col xs={12}>
              <DropDownListItem
                title={course.name}
                handleEdit={() => updateCourse(course)}
                handleDelete={() => deleteCourse(course.id)}
                handleCreate={() => createDanceClass(course)}
                handleClick={() => handleClick(course.id)}
              />
            </Col>
            <Col xs={0}> </Col>
          </Row>
        ))}
      </DropDownList>
      <Pagination
        count={!state.data ? Math.ceil(count / 10) : 0}
        page={!state.data ? page : 1}
        onChange={handleChangeCoursePage}
        shape='rounded'
      />
    </>
  )
}

export default CourseList
