import React from 'react'
import List from '@material-ui/core/List'
import { Divider } from '@material-ui/core'

const Container = ({ children }) => (
  <List
  className="drop-down-list"
    component='nav'
    aria-labelledby='nested-list-subheader'
  >
    <Divider />
    {children}
  </List>
)

export default Container
