import React, { useEffect } from "react"
import Header from "./components/Header"
import Session from "./components/Session"
import useSessions from "./hooks/useSessions"
import useConnection from "./hooks/useConnection"
import { getWindowName, addSession, removeSession } from "./scripts/session"
import "./custom.css"
import SessionExpiration from "./components/SessionExpiration"

const App = () => {
	const [current, sessions, setOthers, refresh] = useSessions()
	
	useConnection(notifySessions => setOthers(notifySessions))

	useEffect(async () => {
		if (!window.name) {
			window.name = await getWindowName()
			if (!await addSession())
				throw new Error("An error occurred adding session.")
			
			refresh()
		}
	}, [])

	return (
		<React.Fragment>
			<Header />
			<div className={"content"}>
				<Session 
					session={current}
					sessions={sessions}
					refresh={refresh}
				/>
				<SessionExpiration 
					expiresIn={current.detail.expiresIn}
				/>
			</div>
		</React.Fragment>
	)
}

export default App
