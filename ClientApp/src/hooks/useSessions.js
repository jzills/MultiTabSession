import { useState, useEffect } from "react"
import { getSessions } from "../utilities/session"

const useSessions = () => {
    const [sessions, setSessions] = useState([])

    useEffect(() => refresh(), [])

    const refresh = async () => {
        const sessions = await getSessions()
        setSessions(sessions)
    }

	return [sessions, refresh]
}

export default useSessions