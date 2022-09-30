const sessionHeaders = Object.freeze({
    INITIALIZE_SESSION: "x-init-session",
    FROM_PREVIOUS_SESSION: "x-from-previous-session",
    SESSION: "x-session"
})

const sessionRoutes = Object.freeze({
    ADD: "session",
    REMOVE: "session",
    CURRENT: "session",
    ALL: "session/all",
    WINDOW: "session/window"
})

export { sessionHeaders, sessionRoutes }