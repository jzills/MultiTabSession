const handleCreated = (connection, handler) => {
    const onCreated = otherSessionIds => 
        handler(prevOtherSessionIds => [
            ...prevOtherSessionIds, 
            ...otherSessionIds
        ])

    connection.on("created", message => onCreated(message))
}

const handleRemoved = (connection, handler) => {
    const onRemoved = otherSessionId => {
        handler(prevOtherSessions => {
            const index = prevOtherSessions
                .indexOf(sessionId => sessionId === otherSessionId)

            const tempOtherSessions = [...prevOtherSessions]
            tempOtherSessions.splice(index, 1)
            return tempOtherSessions
        })
    }

    connection.on("removed", message => onRemoved(message))
}

export { handleCreated, handleRemoved }