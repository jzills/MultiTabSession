import React, { useState, useEffect } from 'react';
import Session from './components/Session';
import { getWindowName, addSession } from './utilities/session';
import useSessions from "./hooks/useSessions"
import "./custom.css"
import Header from './components/Header';
import useConnection from './hooks/useConnection';

const App = () => {
	const [isLoading, setIsLoading] = useState(true)
	const [current, sessions, setSessions, refresh] = useSessions()
	
	useConnection(notifySessions => setSessions(notifySessions))

	useEffect(async () => {
		if (!window.name) {
			window.name = await getWindowName()
			if (!await addSession())
				throw new Error("An error occurred adding session.")

			refresh()
		}

		setIsLoading(false)
	}, [])

	return (
		<React.Fragment>
			<Header />
			<div className={"content"}>
				<Session 
					session={current}
					sessions={sessions}
					refresh={refresh}
					isLoading={isLoading}
				/>
			</div>
		</React.Fragment>
	)
}

export default App
