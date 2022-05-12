import { useState, useEffect } from "react"
import { getSession } from "../utilities/session"
import { convertToArray } from "../utilities/dataConversion"

const useSession = () => {  
    const [session, setSession] = useState({
        detail: {},
        applicationState: [],
        isLoading: true
    })

    useEffect(() => refresh(), [])

    const refresh = async () => {
        const session = await getSession()
        const applicationState = convertToArray(session.applicationState)
        delete session.applicationState
        setSession({
            detail: {...session},
            applicationState: applicationState,
            isLoading: false
        })
    }

	return [session, refresh]
}

export default useSession