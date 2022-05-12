import React from "react"
import SessionCard from "./SessionCard"

const SessionDetail = ({data}) => {
    return (
        <React.Fragment>
            <h1>Session Detail</h1>
            <SessionCard data={data} />
        </React.Fragment>
    )
}

export default SessionDetail