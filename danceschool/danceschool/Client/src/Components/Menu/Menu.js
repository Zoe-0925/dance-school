import React from 'react'
import ClickAwayListener from '@material-ui/core/ClickAwayListener'
import Grow from '@material-ui/core/Grow'
import Paper from '@material-ui/core/Paper'
import Popper from '@material-ui/core/Popper'

const Menu = ({ open, handleClose, anchorRef, children }) => {
  return (
    <Popper
      open={open}
      anchorEl={anchorRef.current}
      transition
      disablePortal
    >
      {({ TransitionProps, placement }) => (
        <Grow
          {...TransitionProps}
          style={{
            transformOrigin: placement === 'bottom'
          }}
        >
          <Paper>
            <ClickAwayListener onClickAway={handleClose}>
              {children}
            </ClickAwayListener>
          </Paper>
        </Grow>
      )}
    </Popper>
  )
}

export default Menu
