import { getWindowName, addSession } from "../utilities/session"

const useSession = () => {  
    const [state, setState] = useState({
		windowName: null,
		isLoading: true
	})

	useEffect(async () => {
		if (!state.windowName) {
			const windowName = await getWindowName()
			if (!await addSession())
				throw new Error("An error occurred adding session.")

			setState({
				windowName: windowName, 
				isLoading: false
			})
		}
	})
}