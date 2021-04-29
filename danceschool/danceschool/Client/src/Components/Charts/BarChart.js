import { Bar } from 'react-chartjs-2'
import {
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Divider,
  useTheme,
  MenuList,
  MenuItem
} from '@material-ui/core'
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown'
import Menu from '../Menu/Menu'

const BarChart = ({
  data,
  open,
  handleOpen,
  handleChange,
  handleClose,
  anchorRef
}) => {
  const theme = useTheme()

  const options = {
    animation: false,
    cornerRadius: 20,
    layout: { padding: 0 },
    legend: { display: false },
    maintainAspectRatio: false,
    responsive: true,
    scales: {
      xAxes: [
        {
          barThickness: 20,
          maxBarThickness: 15,
          barPercentage: 0.5,
          categoryPercentage: 0.5,
          ticks: {
            fontColor: theme.palette.text.secondary
          },
          gridLines: {
            display: false,
            drawBorder: false
          }
        }
      ],
      yAxes: [
        {
          ticks: {
            fontColor: theme.palette.text.secondary,
            beginAtZero: true,
            min: 0
          },
          gridLines: {
            borderDash: [2],
            borderDashOffset: [2],
            color: theme.palette.divider,
            drawBorder: false,
            zeroLineBorderDash: [2],
            zeroLineBorderDashOffset: [2],
            zeroLineColor: theme.palette.divider
          }
        }
      ]
    },
    tooltips: {
      backgroundColor: theme.palette.background.paper,
      bodyFontColor: theme.palette.text.secondary,
      borderColor: theme.palette.divider,
      borderWidth: 1,
      enabled: true,
      footerFontColor: theme.palette.text.secondary,
      intersect: false,
      mode: 'index',
      titleFontColor: theme.palette.text.primary
    }
  }

  return (
    <Card className='chart'>
      <CardHeader
        className='card-header'
        action={
          <Button
            endIcon={<ArrowDropDownIcon />}
            size='small'
            variant='text'
            onClick={handleOpen}
            ref={anchorRef}
          >
            <p className='bar-chart-btn'>
              {data && data.header ? data.header : 'This Week'}
            </p>
          </Button>
        }
        title='Latest Bookings'
      />
      <Menu open={open} handleClose={handleClose} anchorRef={anchorRef}>
        <MenuList autoFocusItem={open} id='menu-list-grow'>
          {data && data.header && data.header !== 'This week' && (
            <MenuItem onClick={() => handleChange('week')}>This Week</MenuItem>
          )}
          {data && data.header && data.header !== 'This month' && (
            <MenuItem onClick={() => handleChange('month')}>
              This Month
            </MenuItem>
          )}
          {data && data.header && data.header !== 'This year' && (
            <MenuItem onClick={() => handleChange('year')}>This Year</MenuItem>
          )}
        </MenuList>
      </Menu>
      <Divider />
      <CardContent>
        <Box className='box'>
          {data && <Bar data={data} options={options} />}
        </Box>
      </CardContent>
      <Divider />
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'flex-end',
          p: 2
        }}
      ></Box>
    </Card>
  )
}

export default BarChart
