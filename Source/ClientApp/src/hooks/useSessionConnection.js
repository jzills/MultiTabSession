import { useState, useEffect } from "react"
import useConnection from "./useConnection"
import { sessionHeaders } from "../scripts/sessionConstants"
import { handleCreated, handleRemoved } from "../scripts/sessionHandlers"

const useSessionConnection = (clientSessionId, setOtherSessions) => {
    const [urlBuilder, setUrlBuilder] = useState({
        base: `https://localhost:44432`,
        path: "hubs",
        query: `${sessionHeaders.SESSION}`,
        queryArg: clientSessionId
    })

    const [connection, setStartConnection] = 
        useConnection(urlBuilder, [
            connection => handleCreated(connection, setOtherSessions), 
            connection => handleRemoved(connection, setOtherSessions)
        ])

    useEffect(() => {
		if (clientSessionId) {
            setUrlBuilder(prevUrlBuilder => ({
                ...prevUrlBuilder, 
                queryArg: clientSessionId
            }))

            setStartConnection(true)
		}
	}, [clientSessionId])

    return [connection]
} 

export default useSessionConnection