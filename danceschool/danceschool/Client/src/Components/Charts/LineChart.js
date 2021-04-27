import Chart from 'react-google-charts'

const LineChart = ({ data, title, axis, yLabel }) => (
  <Chart
    width={'100%'}
    height={'300'}
    chartType='Line'
    loader={<div>Loading Chart</div>}
    data={data}
    options={{
      chart: {
        title: { title }
      },
      width: 900,
      height: 300,
      series: {
        // Gives each series an axis name that matches the Y-axis below.
        0: { axis: { axis } }
      },
      axes: {
        // Adds labels to each axis; they don't have to match the axis names.
        y: { yLabel }
      }
    }}
  />
)

export default LineChart
