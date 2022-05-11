import React from "react"

const SessionDetail = ({data}) => {
    return (
        <React.Fragment>
            <h1>Session Detail</h1>
            {
                Object.keys(data).map(key => <div>{key}: {data[key]}</div>)
            }
        </React.Fragment>
    )
}

export default SessionDetail