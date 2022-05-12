import { sessionHeaders } from "./sessionHeaders"

async function addSession() {
    const sessionId = sessionStorage.getItem("sessionId")
    const response = await fetch("session/addsession", {
        method: "POST", 
        headers: sessionId ? { 
            [sessionHeaders.INITIALIZE_SESSION]: window.name,
            [sessionHeaders.FROM_PREVIOUS_SESSION]: sessionId
        } : { [sessionHeaders.INITIALIZE_SESSION]: window.name }
    })

    response.ok ? 
        sessionStorage.setItem("sessionId", window.name) : 
        console.error(`${response.status}: ${response.statusText}`)

    return response.ok
}

async function getWindowName() {
    const response = await fetch("session/getwindowname")
    if (response.ok) {
        const { windowName } = await response.json()
        return windowName
    } else {
        console.error(`${response.status}: ${response.statusText}`)
    }
}

async function getSession() {
    const response = await fetch("session/getsession", {
        headers: { [sessionHeaders.SESSION]: window.name }
    })

    return response.ok ? 
        response.json() : 
        console.error(`${response.status}: ${response.statusText}`)
}

export { addSession, getSession, getWindowName }