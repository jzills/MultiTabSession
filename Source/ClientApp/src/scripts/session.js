import { sessionHeaders, sessionRoutes } from "./sessionConstants"

async function addSession(initialClientSessionId) {
    const previousSessionId = sessionStorage.getItem("sessionId")
    const response = await fetch(sessionRoutes.ADD, {
        method: "POST", 
        headers: previousSessionId ? { 
            [sessionHeaders.INITIALIZE_SESSION]: initialClientSessionId,
            [sessionHeaders.FROM_PREVIOUS_SESSION]: previousSessionId
        } : { [sessionHeaders.INITIALIZE_SESSION]: initialClientSessionId }
    })

    response.ok ? 
        sessionStorage.setItem("sessionId", initialClientSessionId) : 
        console.error(`${response.status}: ${response.statusText}`)

    return response.ok
}

async function removeSession() {
    const sessionId = sessionStorage.getItem("sessionId") || window.name
    if (sessionId) {
        const response = await fetch(sessionRoutes.REMOVE, {
            method: "DELETE", 
            headers: { [sessionHeaders.SESSION]: sessionId },
            keepalive: true
        })

        return response.ok
    } 
}

async function getClientSessionId() {
    const response = await fetch(sessionRoutes.WINDOW)
    if (response.ok) {
        const { clientSessionId } = await response.json()
        return clientSessionId
    } else {
        console.error(`${response.status}: ${response.statusText}`)
    }
}

async function getSession() {
    const response = await fetch(sessionRoutes.CURRENT, {
        headers: { [sessionHeaders.SESSION]: window.name }
    })

    return response.ok ? 
        response.json() : 
        console.error(`${response.status}: ${response.statusText}`)
}

async function getSessions() {
    const response = await fetch(sessionRoutes.ALL)

    return response.ok ? 
        response.json() : 
        console.error(`${response.status}: ${response.statusText}`)
}

export { addSession, removeSession, getSession, getSessions, getClientSessionId }