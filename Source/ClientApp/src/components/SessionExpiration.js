import { useState, useEffect } from "react"
import useSession from "../hooks/useSession"

const SessionExpiration = ({expiresIn}) => {
    const [state, setState] = useSession()

    useEffect(() => {
        const interval = setInterval(() => {
            debugger
            setState(prevState => prevState + 1)
        }, 1000)

        return () => clearInterval(interval);
    }, [expiresIn])

    return (
        <div>
            {state}
        </div>
    )
}

export default SessionExpiration