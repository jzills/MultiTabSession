import React, { useState, useEffect } from 'react';
import Session from './components/Session';
import "./custom.css"
import { getWindowName, addSession } from './utilities/session';

const App = () => {
	const [state, setState] = useState({ isLoading: true })

	useEffect(async () => {
		if (!window.name) {
			window.name = await getWindowName()
			if (!await addSession())
				throw new Error("An error occurred adding session.")
		}

		setState({ isLoading: false })
	}, [])

	return (
		state.isLoading ? <h1>Loading</h1> : <Session />
	)
}

export default App
