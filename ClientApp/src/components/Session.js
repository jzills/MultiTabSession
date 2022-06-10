import React from "react"
import SessionDetail from "./SessionDetail"
import SessionTable from "./SessionTable"
import SessionOther from "./SessionOther"
import Loading from "./Loading"

const Session = ({session, sessions, refresh}) =>
    session.isLoading ? 
        <Loading /> :
        <React.Fragment>
            <div className={"session-detail"}>
                <SessionDetail 
                    data={session.detail} 
                    header={"Session Detail"}
                />
                <SessionOther 
                    data={sessions}
                    header={"Other Sessions"}
                />
            </div>
            <SessionTable  
                data={session.applicationState}
                header={"Session Variables"}
                refresh={refresh}
            />       
        </React.Fragment>

export default Session