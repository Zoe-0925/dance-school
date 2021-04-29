import React, { useState } from 'react'
import Drawer from './Drawer'
import { DrawerLinks } from './DrawerLinks'
import NavBar from '../NavBar/NavBar'

const DrawerContainer = ({
  handleClick,
  currentLocation = 'Analytics',
  role = '',
  ...props
}) => {
  const [open, setOpen] = useState(true)

  const toggleDrawer = () => setOpen(!open)
  console.log("role", role)

  return (
    <div className={open ? 'main drawer-close' : 'main drawer-open'}>
      <NavBar toggleDrawer={toggleDrawer} />
      <Drawer handleClick={toggleDrawer} open={open} role={role}>
        <DrawerLinks
          handleClick={handleClick}
          currentLocation={currentLocation}
          role={role}
        />
      </Drawer>
      {props.children}
    </div>
  )
}

export default React.memo(
  DrawerContainer,
  (prevProps, nextProps) =>
    prevProps.type !== nextProps.type &&
    prevProps.currentLocation !== nextProps.currentLocation
)
