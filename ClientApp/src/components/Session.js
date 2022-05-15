import React from "react"
import SessionDetail from "./SessionDetail"
import SessionTable from "./SessionTable"
import useSession from "../hooks/useSession"
import useTable from "../hooks/useTable"
import Loading from "./Loading"

const Session = () => {
    const [session, refresh] = useSession()
    const [onRowAdd, onRowUpdate, onRowDelete] = useTable(refresh)

    return (
        session.isLoading ? 
            <Loading /> :
            <React.Fragment>
                <SessionDetail data={session.detail} />
                <SessionTable  
                    data={session.applicationState}
                    onRowAdd={onRowAdd}
                    onRowUpdate={onRowUpdate}
                    onRowDelete={onRowDelete}
                />       
            </React.Fragment>
    )
}

export default Session