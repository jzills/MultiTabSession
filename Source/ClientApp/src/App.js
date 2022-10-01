import React, { useEffect, useState } from "react"
import Header from "./components/Header"
import Session from "./components/Session"
import { getClientSessionId, addSession } from "./scripts/session"
import "./custom.css"
import SessionExpiration from "./components/SessionExpiration"
import useSession from "./hooks/useSession"
import useSessionConnection from "./hooks/useSessionConnection"

const App = () => {
	const [session, refresh] = useSession()
	const [otherSessions, setOtherSessions] = useState([])

	useSessionConnection(
		session.detail.clientSessionId, 
		setOtherSessions
	)

	useEffect(async () => {
		if (!window.name) {
			window.name = await getClientSessionId()
			if (!await addSession(window.name))
				throw new Error("An error occurred adding session.")
			
			refresh()
		}
	}, [])

	return (
		<React.Fragment>
			<Header />
			<div className={"content"}>
				<Session 
					session={session}
					sessions={otherSessions}
					refresh={refresh}
				/>
				{/* <SessionExpiration 
					expiresIn={current.detail.expiresIn}
				/> */}
			</div>
		</React.Fragment>
	)
}

export default App
