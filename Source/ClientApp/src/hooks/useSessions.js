// import { useState, useEffect } from "react"
// import { getSessions } from "../scripts/session"
// import { convertToArray } from "../scripts/dataConversion"

// const useSessions = () => {
//     const [sessions, setSessions] = useState([])
//     const [current, setCurrent] = useState({
//         detail: {},
//         applicationState: [],
//         isLoading: true
//     })

//     useEffect(() => refresh(), [])

//     const setOthers = sessions => setSessions(sessions.filter(session => session.windowName !== window.name))

//     const refresh = async () => {
//         const sessions = await getSessions()
//         const session = getCurrent(sessions)
//         setOthers(sessions)

//         const applicationState = convertToArray(session.applicationState)
//         delete session.applicationState
//         setCurrent({
//             detail: {...session},
//             applicationState: applicationState,
//             isLoading: false
//         })
//     }

//     const getCurrent = sessions => {
//         const session = sessions.filter(session => session.windowName === window.name)
//         if (session && session.length)
//             return session[0]
//         else throw new Error("No current session found.")
//     }

// 	return [current, sessions, setOthers, refresh]
// }

// export default useSessions