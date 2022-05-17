import MaterialTable from "material-table";
import tableIcons from "../utilities/tableIcons";

const SessionTable = ({data, onRowAdd, onRowUpdate, onRowDelete}) => {
    return (
        <div className={"session-table"}>
            <h4>Session Variables</h4>
            <MaterialTable
                title={""}
                icons={tableIcons}
                columns={[
                    { title: "Key", field: "key" },
                    { title: "Value", field: "value" }
                ]}
                data={data}
                editable={{
                    onRowAdd: onRowAdd(data),
                    onRowUpdate: onRowUpdate(data),
                    onRowDelete: onRowDelete(data)
                }}
            />
        </div>
    )
}

export default SessionTable