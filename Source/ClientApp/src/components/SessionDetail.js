import React from "react"
import SessionCard from "./SessionCard"
import { Box } from "@material-ui/core"

const SessionDetail = ({data, header}) => 
    <div>
        <h4>{header}</h4>
        <Box
            display="flex"
            justifyContent="center"
            alignItems="center"
            minWidth="200px"
        >
            <SessionCard data={data} />
        </Box> 
    </div>

export default SessionDetail