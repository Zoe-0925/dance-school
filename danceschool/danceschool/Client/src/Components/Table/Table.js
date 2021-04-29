import React from 'react'
import {
    Table, TableBody, TableCell, TableContainer, TableHead,
    TableRow, Paper
} from '@material-ui/core'
import { v4 as uuidv4 } from 'uuid'

const MyTable = ({ tableHeader=[], children }) => (
    <div key={uuidv4()} className="project-list-table">
        <TableContainer key={uuidv4()} component={Paper}>
            <Table className="project-list-table" aria-label="simple table">
                <TableHead>
                    <TableRow >
                        {tableHeader && tableHeader.map(each => <TableCell key={uuidv4()} align="left">{each}</TableCell>)}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {children}
                </TableBody>
            </Table>
        </TableContainer>
    </div>
)

export default MyTable
