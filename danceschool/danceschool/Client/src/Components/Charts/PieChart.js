import { Doughnut } from 'react-chartjs-2'
import {
  Box,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Typography,
  useTheme
} from '@material-ui/core'
import { Row, Col } from 'reactstrap'
import { v4 as uuidv4 } from 'uuid'

const PieChart = ({ data = [], memberships = [] }) => {
  const theme = useTheme()

  const options = {
    animation: false,
    cutoutPercentage: 80,
    layout: { padding: 0 },
    legend: {
      display: false
    },
    maintainAspectRatio: false,
    responsive: true,
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
    <Card className="chart">
      <CardHeader className="card-header" title='Traffic by Membership' />
      <Divider />
      <CardContent>
        <Box className="pie-box">
          <Doughnut data={data} options={options} />
        </Box>
        <Box>
          <Row>
            {memberships.map(({ color, title, value }) => (
              <Col xs={3} key={uuidv4()}>
                <Box
                  key={title}
                  sx={{
                    p: 1,
                    textAlign: 'center'
                  }}
                >
                  <Typography color='textPrimary' variant='body1'>
                    {title}
                  </Typography>
                  <Typography style={{ color }} variant='h6'>
                    {value}%
                  </Typography>
                </Box>
              </Col>
            ))}
          </Row>
        </Box>
      </CardContent>
    </Card>
  )
}

export default PieChart
