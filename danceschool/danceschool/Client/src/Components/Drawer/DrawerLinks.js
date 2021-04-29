import React, { Fragment } from 'react'
import {
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Link
} from '@material-ui/core'
import PeopleIcon from '@material-ui/icons/People'
import GroupIcon from '@material-ui/icons/Group'
import EmojiPeopleIcon from '@material-ui/icons/EmojiPeople'
import CardMembershipIcon from '@material-ui/icons/CardMembership'
import CollectionsBookmarkIcon from '@material-ui/icons/CollectionsBookmark'
import SubscriptionsIcon from '@material-ui/icons/Subscriptions'
import FindInPageIcon from '@material-ui/icons/FindInPage'

export const MyListItem = ({
  color,
  handleClick,
  id,
  selected,
  primary,
  children
}) => (
  <Link color={color} onClick={handleClick}>
    <ListItem button key={id} selected={selected}>
      <ListItemIcon> {children}</ListItemIcon>
      <ListItemText primary={primary} />
    </ListItem>
  </Link>
)

export const DrawerLinks = ({ currentLocation, handleClick, role = '' }) => (
  <Fragment>
    <List className='drawer-links'>
      {role === 'admin' && (
        <>
          <MyListItem
            color={currentLocation !== 'Analytics' ? 'inherit' : 'primary'}
            handleClick={() => handleClick('Analytics')}
            id='Analytics'
            selected={currentLocation === 'Analytics'}
            primary='Analytics'
          >
            <FindInPageIcon />
          </MyListItem>
          <br />
          <br />
        </>
      )}
      <MyListItem
        color={currentLocation !== 'Courses' ? 'inherit' : 'primary'}
        handleClick={() => handleClick('Courses')}
        id='Courses'
        selected={currentLocation === 'Courses'}
        primary='Courses'
      >
        <GroupIcon />
      </MyListItem>
      <MyListItem
        color={currentLocation !== 'Memberships' ? 'inherit' : 'primary'}
        handleClick={() => handleClick('Memberships')}
        id='Memberships'
        selected={currentLocation === 'Memberships'}
        primary='Memberships'
      >
        <CardMembershipIcon />
      </MyListItem>
      {role === 'admin' && (
        <MyListItem
          color={currentLocation !== 'Students' ? 'inherit' : 'primary'}
          handleClick={() => handleClick('Students')}
          id='Students'
          selected={currentLocation === 'Students'}
          primary='Students'
        >
          <PeopleIcon />
        </MyListItem>
      )}
      <MyListItem
        color={currentLocation !== 'Bookings' ? 'inherit' : 'primary'}
        handleClick={() => handleClick('Bookings')}
        id='Bookings'
        selected={currentLocation === 'Bookings'}
        primary='Bookings'
      >
        <CollectionsBookmarkIcon />
      </MyListItem>
      {role === 'admin' && (
        <MyListItem
          color={currentLocation !== 'Instructors' ? 'inherit' : 'primary'}
          handleClick={() => handleClick('Instructors')}
          id='Instructors'
          selected={currentLocation === 'Instructors'}
          primary='Instructors'
        >
          <EmojiPeopleIcon />
        </MyListItem>
      )}
      <MyListItem
        color={currentLocation !== 'Subscriptions' ? 'inherit' : 'primary'}
        handleClick={() => handleClick('Subscriptions')}
        id='Subscriptions'
        selected={currentLocation === 'Subscriptions'}
        primary='Subscriptions'
      >
        <SubscriptionsIcon />
      </MyListItem>
    </List>
  </Fragment>
)

/**
 * <MyListItem
        color={currentLocation !== 'Subscriptions' ? 'inherit' : 'primary'}
        handleClick={() => handleClick('Subscriptions')}
        id='Subscriptions'
        selected={currentLocation === 'Subscriptions'}
        primary='Subscriptions'
      >
        <SubscriptionsIcon />
      </MyListItem>
 */
