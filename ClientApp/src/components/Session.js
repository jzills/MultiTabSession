import React, { useEffect, useState } from "react"
import SessionDetail from "./SessionDetail"
import SessionTable from "./SessionTable"
import { getSession } from "../utilities/session"

const convertToArray = (dictionary) => 
    [...Object.keys(dictionary).map(key => ({ key: key, value: dictionary[key] }))]

const convertToDictionary = array => 
    Object.assign({}, ...array.map(element => ({[element.key]: element.value})))

const Session = () => {
    const [state, setState] = useState({
        sessionDetail: {},
        applicationState: {},
        isLoading: true
    })

    useEffect(() => refreshApplicationState(), [])

    const refreshApplicationState = async () => {
        const session = await getSession()
        setState({
            sessionDetail: {
                "id": session.id,
                "windowName": session.windowName
            },
            applicationState: convertToArray(session.applicationState),
            isLoading: false
        })
    }

    const addApplicationVariable = data => 
        new Promise((resolve, reject) => {
            const temp = [...state.applicationState, data]
            const applicationState = convertToDictionary(temp)
            setTimeout(async () => {
                const response = await fetch("session/batchupdateapplicationstate", {
                    method: "post",
                    headers: { 
                        "content-type": "application/json",
                        "x-session": window.name 
                    },
                    body: JSON.stringify(applicationState)
                })

                if (response.ok) {
                    await refreshApplicationState()
                    resolve()
                } else {
                    reject()
                }
            }, 1000);
        })

    const updateApplicationVariable = (data, prevData) => 
        new Promise((resolve, reject) => {
            const temp = [...state.applicationState]
            temp[prevData.tableData.id] = data
            const applicationState = convertToDictionary(temp)
            setTimeout(async () => {
                const response = await fetch("session/batchupdateapplicationstate", {
                    method: "post",
                    headers: { 
                        "content-type": "application/json",
                        "x-session": window.name 
                    },
                    body: JSON.stringify(applicationState)
                })

                if (response.ok) {
                    await refreshApplicationState()
                    resolve()
                } else {
                    reject()
                }
            }, 1000);
        })

    const deleteApplicationVariable = data => 
        new Promise((resolve, reject) => {
            setTimeout(() => {
                resolve();
            }, 1000);
        })

    return (
        state.isLoading ? <h1>again</h1> :
        <React.Fragment>
            <SessionDetail data={state.sessionDetail} />
            <SessionTable  
                data={state.applicationState}
                onRowAdd={addApplicationVariable}
                onRowUpdate={updateApplicationVariable}
                onRowDelete={deleteApplicationVariable}
            />       
        </React.Fragment>
    )
}

export default Session