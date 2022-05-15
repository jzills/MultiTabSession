import React, { useState, useEffect } from 'react';
import Session from './components/Session';
import SessionOther from "./components/SessionOther"
import { getWindowName, addSession } from './utilities/session';
import useSessions from "./hooks/useSessions"
import "./custom.css"
import Loading from './components/Loading';
import useTable from './hooks/useTable';
import SessionDetail from './components/SessionDetail';
import SessionTable from './components/SessionTable';
import Header from './components/Header';

const App = () => {
	const [isLoading, setIsLoading] = useState(true)
	const [sessions, current, refresh] = useSessions()
	const [onRowAdd, onRowUpdate, onRowDelete] = useTable(refresh)

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
			{
				isLoading ? 
					<Loading /> : 
					<div class="content">
						<SessionDetail data={current.detail} sessions={sessions} />
						<SessionTable  
							data={current.applicationState}
							onRowAdd={onRowAdd}
							onRowUpdate={onRowUpdate}
							onRowDelete={onRowDelete}
						/> 
					</div>
			}
		</React.Fragment>
	)
}

export default App
