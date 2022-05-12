import React from "react"
import SessionDetail from "./SessionDetail"
import SessionTable from "./SessionTable"
import { CircularProgress } from "@material-ui/core"
import { convertToDictionary } from "../utilities/dataConversion"
import useSession from "../hooks/useSession"
import useTable from "../hooks/useTable"

const Session = () => {
    const [session, refresh] = useSession()
    const [onRowAdd, onRowUpdate, onRowDelete] = useTable(refresh)

    return (
        session.isLoading ? 
            <CircularProgress /> :
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