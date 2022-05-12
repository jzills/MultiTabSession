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
        setSession({
            detail: {
                "id": session.id,
                "windowName": session.windowName
            },
            applicationState: convertToArray(session.applicationState),
            isLoading: false
        })
    }

	return [session, refresh]
}

export default useSession