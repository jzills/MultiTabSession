import React, { useState, useEffect } from "react"
import Header from "./components/Header"
import Session from "./components/Session"
import useSessions from "./hooks/useSessions"
import useConnection from "./hooks/useConnection"
import { getWindowName, addSession } from "./utilities/session"
import "./custom.css"

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
			</div>
		</React.Fragment>
	)
}

export default App
