import React from "react"
import SessionCard from "./SessionCard"
import { Box } from "@material-ui/core"
import SessionOther from "./SessionOther"

const SessionDetail = ({data, header}) => {
    return (
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
    )
}

export default SessionDetail