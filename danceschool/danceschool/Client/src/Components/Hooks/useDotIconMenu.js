import { useState, useRef } from 'react'

export const useDotIconMenu = () => {
    const [anchorEl, setAnchorEl] = useState(null)
    const isOpen = Boolean(anchorEl)
    const anchorRef = useRef(null)
  
    const handleMenuClose = () => {
      setAnchorEl(null)
    }
  
    const handleMenuOpen = event => {
      setAnchorEl(event.currentTarget)
    }
  
    return { anchorEl, isOpen, anchorRef, handleMenuClose, handleMenuOpen }
  }

  