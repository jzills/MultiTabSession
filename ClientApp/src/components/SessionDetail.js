import React from "react"
import SessionCard from "./SessionCard"
import { Box } from "@material-ui/core"
import SessionOther from "./SessionOther"

const SessionDetail = ({data, sessions}) => {
    return (
        <div className={"session-detail"}>
            <div>
                <h4>Session Detail</h4>
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
                <h4>Other Sessions</h4>
                <SessionOther data={sessions} />
            </div>
        </div>
    )
}

export default SessionDetail