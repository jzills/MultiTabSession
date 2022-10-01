import { useState, useEffect } from "react"
import { HubConnectionBuilder } from "@microsoft/signalr"

const useConnection = (urlBuilder, connectionHandlers) => {
    const [connection, setConnection] = useState(null)
	const [startConnection, setStartConnection] = useState(false)

	useEffect(() => {
		if (startConnection) {
			const connection = new HubConnectionBuilder()
				.withUrl(`${urlBuilder.base}/${urlBuilder.path}?${urlBuilder.query}=${urlBuilder.queryArg}`)
				.withAutomaticReconnect()
				.build()

			setConnection(connection)
		}
	}, [startConnection])

    useEffect(() => {
		if (connection) {
			for (const connectionHandler of connectionHandlers) {
				connectionHandler(connection)
			}

			connection.start()
		}
	}, [connection])

    return [connection, setStartConnection]
}

export default useConnection