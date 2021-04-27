import { format, formatISO, add, parseJSON } from 'date-fns'
import { colors } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'

export const formatDate = dateString => {
  try {
    const date = parseJSON(dateString)
    return format(date, 'MMMM dd yyyy')
  } catch (err) {
    return ''
  }
}

export const formatTime = dateString => {
  try {
    const date = parseJSON(dateString)
    return format(date, 'h bbb')
  } catch (err) {
    return ''
  }
}

export const generateDateString = () => {
  try {
    return formatISO(new Date(), { representation: 'date' })
  } catch (err) {
    return ''
  }
}

export const generateTimeString = () => {
  try {
    return formatISO(new Date(), { representation: 'date' })
  } catch (err) {
    return ''
  }
}

export const findItemById = (list = [], id = '') => {
  const result = list.find(item => item._id === id)
  return result
}

export function createHeader (token) {
  if (!token || token === '') {
    return
  }

  return {
    headers: {
      'Access-Control-Allow-Origin': '*',
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Authorization: token //deprecated: "Bearer " + token
    }
  }
}

export function generateDateList (startDate, occurrence) {
  let list = [formatDate(startDate)]
  if (occurrence > 1) {
    for (let i = 1; i < occurrence; i++) {
      const nextDate = add(startDate, { days: 7 })
      list = [...list, formatDate(nextDate)]
    }
  }
  return list
}

export function getWeekday (num) {
  switch (num) {
    case 0:
      return 'Sunday'
    case 1:
      return 'Monday'
    case 2:
      return 'Tuesday'
    case 3:
      return 'Wednesday'
    case 4:
      return 'Thursday'
    case 5:
      return 'Friday'
    case 6:
      return 'Saturday'
    default:
      return ''
  }
}

export const getColor = weekday => {
  let result = 'status-tab short-tab '
  switch (weekday) {
    case 'Monday':
      return result + 'yellow '
    case 'Tuesday':
      return result + 'grey '
    case 'Wednesday':
      return result + 'purple '
    case 'Thursday':
      return result + 'blue '
    case 'Friday':
      return result + 'black '
    case 'Saturday':
      return result + 'pink '
    default:
      return ''
  }
}

export const convertDuration = duration => {
  switch (duration) {
    case 'weekly':
      return { weeks: 1 }
    case 'monthly':
      return { months: 1 }
    case 'quarterly':
      return { months: 3 }
    case 'annually':
      return { years: 1 }
    default:
      return 0
  }
}

export const getWeekDayfromDate = date => {
  return getWeekday(parseJSON(date).getDay())
}

export const getClassOptions = (startDate, numberOfOccurence) => {
  if (numberOfOccurence > 0) {
    let options = [{ label: formatDate(startDate), value: startDate }]
    for (let i = 1; i < numberOfOccurence; i++) {
      const nextOccurence = add(startDate, { weeks: i })
      options = [
        ...options,
        { label: formatDate(nextOccurence), nextOccurence }
      ]
    }
    return options
  }
}

export const getMonth = num => {
  switch (num) {
    case 1:
      return 'Jan'
    case 2:
      return 'Feb'
    case 3:
      return 'Mar'
    case 4:
      return 'Apr'
    case 5:
      return 'May'
    case 6:
      return 'Jun'
    case 7:
      return 'Jul'
    case 8:
      return 'Aug'
    case 9:
      return 'Sep'
    case 10:
      return 'Oct'
    case 11:
      return 'Nov'
    case 12:
      return 'Dec'
    default:
      return ''
  }
}

export const formatPieChartData = (data, label = '') => {
  return {
    datasets: [
      {
        backgroundColor: colors.indigo[500],
        data: data ? data.map(each => each.count) : [],
        label: label
      }
    ],
    labels: data
      ? data.map(each => {
          if (label === 'This week') {
            return formatDate(each.date)
          }
          if (label === 'This year') {
            return getMonth(each.date)
          } else {
            return each.date
          }
        })
      : [],
    header: label
  }
}

export const useButtonStyles = makeStyles({
  root: {
    background:
      'linear-gradient(152deg, rgba(2,124,201,1) 0%, rgba(2,124,201,1) 98%)',
    borderRadius: '0.5rem',
    border: 0,
    color: 'white',
    height: 48,
    padding: '0 30px',
    boxShadow: '0 3px 5px 2px rgba(255, 105, 135, .3)',
    marginTop: 'auto',
    marginBottom: 'auto'
  },
  label: {
    textTransform: 'capitalize'
  }
})
