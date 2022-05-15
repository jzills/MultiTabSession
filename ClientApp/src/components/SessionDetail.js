import React from "react"
import SessionCard from "./SessionCard"
import { Box } from "@material-ui/core"
import SessionOther from "./SessionOther"

const SessionDetail = ({data, sessions}) => {
    return (
        <div class="session-detail">
            <div>
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
            <div style={{paddingTop: "5%"}}>
                <h1>Other Sessions</h1>
                <SessionOther data={sessions} />
            </div>
        </div>
    )
}

export default SessionDetail