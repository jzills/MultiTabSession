import sessionHeaders from "./sessionHeaders"

// window.onload = async () => {
//     debugger
//     if (!window.name) {
//         window.name = await getWindowName()
//         if (!await addSession())
//             throw new Error("An error occurred adding session.")
//     }
// }

async function addSession() {
    const sessionId = sessionStorage.getItem("sessionId")
    const response = await fetch("session/addsession", {
        method: "POST", 
        headers: sessionId ? { 
            "x-init-session": window.name,
            "x-from-previous-session": sessionId
            //[sessionHeaders.initializeSession]: window.name,
            //[sessionHeaders.fromPreviousSession]: sessionId
        } : { "x-init-session": window.name }//[sessionHeaders.initializeSession]: window.name }
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
        headers: {
            //[sessionHeaders.session]: window.name
            "x-session": window.name
        }
    })
    debugger
    return response.ok ? 
        response.json() : 
        console.error(`${response.status}: ${response.statusText}`)
}

export { addSession, getSession, getWindowName }