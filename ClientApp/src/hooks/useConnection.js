import { useState, useEffect } from "react"
import { HubConnectionBuilder } from "@microsoft/signalr"

const useConnection = onNotify => {
    const [connection, setConnection] = useState(null)

	useEffect(() => {
		const connection = new HubConnectionBuilder()
			.withUrl("https://localhost:44432/hubs")
			.withAutomaticReconnect()
			.build()

		setConnection(connection)
	}, [])

    useEffect(() => {
		if (connection) {
			connection.on("Notify", message => onNotify(message))
			connection.start()
		}
	}, [connection])

    return connection
}

export default useConnection