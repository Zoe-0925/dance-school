import React from 'react'
import CreateIcon from '@material-ui/icons/Create'
import ClearIcon from '@material-ui/icons/Clear'
import AddIcon from '@material-ui/icons/Add'
import { DropDownListIconButton } from '../Buttons/IconButtons'
import {
  ListItem,
  ListItemText,
  ListItemIcon,
  ListItemSecondaryAction
} from '@material-ui/core'
import { v4 as uuidv4 } from 'uuid'

const Item = ({
  title,
  handleEdit,
  handleDelete,
  handleCreate,
  className = 'list-item',
  handleClick
}) => {
  return (
    <div className={className} key={uuidv4()}>
      <ListItem button>
        <ListItemIcon> </ListItemIcon>
        <ListItemText
          className='item-text'
          primary={title}
          onClick={handleClick}
        />
        <ListItemSecondaryAction>
          <DropDownListIconButton
            title='Add a dance class'
            handleClick={handleCreate}
          >
            <AddIcon />
          </DropDownListIconButton>
          <DropDownListIconButton
            title='Edit the course'
            handleClick={handleEdit}
          >
            <CreateIcon />
          </DropDownListIconButton>
          <DropDownListIconButton
            title='Delete the course'
            handleClick={handleDelete}
          >
            <ClearIcon />
          </DropDownListIconButton>
        </ListItemSecondaryAction>
      </ListItem>
    </div>
  )
}

export default Item
