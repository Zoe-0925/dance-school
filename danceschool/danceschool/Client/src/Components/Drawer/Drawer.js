import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { Drawer } from '@material-ui/core'
import { ProjectHeaderTab } from '../Buttons/IconButtons'
import CssBaseline from '@material-ui/core/CssBaseline'
import { Row } from 'reactstrap'

const drawerWidth = 240

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex'
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0
  },
  menuButton: {
    marginRight: theme.spacing(2),
    [theme.breakpoints.up('sm')]: {
      display: 'none'
    }
  },
  toolbar: theme.mixins.toolbar,
  drawerPaper: {
    marginTop: '1rem',
    width: drawerWidth,
    backgroundColor: 'rgb(250, 251, 252)'
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3)
  },
  closeMenuButton: {
    marginRight: 'auto',
    marginLeft: 0
  }
}))

const SideDrawer = ({ open, role, children }) => {
  const classes = useStyles()

  return (
    <div className='drawer'>
      <CssBaseline />
      <nav className={classes.drawer}>
        {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
        <Drawer
          className={classes.drawer + ' desktop'}
          variant='persistent'
          open={open}
          classes={{
            paper: classes.drawerPaper
          }}
        >
          <div className={classes.toolbar} />
          <div className='title'>
            <Row>
              {role === 'admin' && (
                <ProjectHeaderTab
                  title='Admin Panel'
                  imgSrc='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSj7Lx0e4d-otfHioR56QV-74WwLUTLucBETw&usqp=CAU'
                />
              )}
              {role === 'student' && (
                <ProjectHeaderTab
                  title='My page'
                  imgSrc='https://cdn4.iconfinder.com/data/icons/smiley-2/54/smiley-face-avatar-happy-plain-512.png'
                />
              )}
            </Row>
          </div>
          {children}
        </Drawer>
      </nav>
    </div>
  )
}
export default SideDrawer
