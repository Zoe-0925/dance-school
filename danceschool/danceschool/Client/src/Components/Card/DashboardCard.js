import {
  Avatar,
  Box,
  Card,
  CardContent,
  Grid,
  Typography
} from '@material-ui/core'
//import ArrowDownwardIcon from '@material-ui/icons/ArrowDownward'

const DashboardCard = ({ title, figure, icon, style = '', content }) => (
  <Card className={'analytics-card-container '}>
    <CardContent>
      <Grid container spacing={3} sx={{ justifyContent: 'space-between' }}>
        <Grid item>
          <Typography color='textSecondary' gutterBottom variant='subtitle1'>
            {title}
          </Typography>
          <Typography color='textPrimary' variant='h4'>
            {figure}
          </Typography>
        </Grid>
        <Grid item>
          {icon && (
            <Avatar className={'card-avatar ' + style} sizes='5rem'>
              {icon}
            </Avatar>
          )}
        </Grid>
      </Grid>
      <Box
        sx={{
          pt: 2,
          display: 'flex',
          alignItems: 'center'
        }}
      >
        {content}
      </Box>
    </CardContent>
  </Card>
)

export default DashboardCard

/**
 *   <ArrowDownwardIcon sx={{ color: red[900] }} />
        <Typography
          sx={{
            color: red[900],
            mr: 1
          }}
          variant='body2'
        >
          12%
        </Typography>
        <Typography color='textSecondary' variant='caption'>
          Since last month
        </Typography>
 */
