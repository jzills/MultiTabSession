import React from "react"
import SessionCard from "./SessionCard"
import { Box } from "@material-ui/core"

const SessionDetail = ({data}) => {
    return (
        <div class="session-detail">
            <h1>Session Detail</h1>
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                minWidth="200px"
            >
                <SessionCard data={data} />
            </Box> 
        </div>
    )
}

export default SessionDetail