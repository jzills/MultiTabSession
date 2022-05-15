import React from "react"
import SessionDetail from "./SessionDetail"
import SessionTable from "./SessionTable"
import useTable from "../hooks/useTable"
import Loading from "./Loading"

const Session = ({session, refresh, isLoading}) => {
    const [onRowAdd, onRowUpdate, onRowDelete] = useTable(refresh)

    return (
        isLoading ? 
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