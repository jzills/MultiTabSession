import React, { useState, useEffect } from 'react';
import Session from './components/Session';
import { getWindowName, addSession } from './utilities/session';
import { CircularProgress } from "@material-ui/core"
import "./custom.css"

const App = () => {
	const [isLoading, setIsLoading] = useState(true)

	useEffect(async () => {
		if (!window.name) {
			window.name = await getWindowName()
			if (!await addSession())
				throw new Error("An error occurred adding session.")
		}

		setIsLoading(false)
	}, [])

	return (
		isLoading ? 
			<CircularProgress /> : 
			<Session />
	)
}

export default App
