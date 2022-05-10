import React, { useEffect, useState } from "react"
import SessionDetail from "./SessionDetail"
import SessionTable from "./SessionTable"
import { getSession } from "../utilities/session"

const Session = () => {
    const [state, setState] = useState({
        sessionDetail: {},
        applicationState: {}
    })

    useEffect(async () => {
        const session = await getSession()
        setState({
            sessionDetail: {
                "id": session.id,
                "windowName": session.windowName
            },
            applicationState: {...session.applicationState}
        })
    }, [])

    const addApplicationVariable = () => 
        new Promise((resolve, reject) => {
            setTimeout(() => {
                resolve();
            }, 1000);
        })

    const deleteApplicationVariable = data => 
        new Promise((resolve, reject) => {
            setTimeout(() => {
                resolve();
            }, 1000);
        })

    return (
        <React.Fragment>
            <SessionDetail data={state.sessionDetail} />
            <SessionTable  
                data={Object.keys(state.applicationState).map(key => ({ key: key, value: state.applicationState[key] }))}
                onRowAdd={addApplicationVariable}
                onRowDelete={deleteApplicationVariable}
            />       
        </React.Fragment>
    )
}

export default Session