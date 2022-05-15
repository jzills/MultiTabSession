import React, { useState, useEffect } from 'react';
import Session from './components/Session';
import SessionOther from "./components/SessionOther"
import { getWindowName, addSession } from './utilities/session';
import { CircularProgress } from "@material-ui/core"
import useSessions from "./hooks/useSessions"
import "./custom.css"

const App = () => {
	const [isLoading, setIsLoading] = useState(true)
	const [sessions, refresh] = useSessions()

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
		isLoading ? 
			<CircularProgress /> : 
			<React.Fragment>
				<Session />
				<SessionOther data={sessions} />
			</React.Fragment>
	)
}

export default App
