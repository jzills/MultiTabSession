import { useState, useEffect } from "react"
import { getSessions } from "../utilities/session"
import { convertToArray } from "../utilities/dataConversion"

const useSessions = () => {
    const [sessions, setSessions] = useState([])
    const [current, setCurrent] = useState({
        detail: {},
        applicationState: [],
        isLoading: true
    })

    useEffect(() => refresh(), [])

    const refresh = async () => {
        const sessions = await getSessions()
        const session = getCurrent(sessions)
        setSessions(sessions.filter(session => session.windowName !== window.name))

        const applicationState = convertToArray(session.applicationState)
        delete session.applicationState
        setCurrent({
            detail: {...session},
            applicationState: applicationState,
            isLoading: false
        })
    }

    const getCurrent = sessions => {
        const session = sessions.filter(session => session.windowName === window.name)
        if (session && session.length)
            return session[0]
        else throw new Error("No current session found.")
    }

	return [current, sessions, setSessions, refresh]
}

export default useSessions