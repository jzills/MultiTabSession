import MaterialTable from "material-table";
import tableIcons from "../utilities/tableIcons";

const SessionTable = ({data, onRowAdd, onRowUpdate, onRowDelete}) => {
    return (
        <div>
            <MaterialTable
                title="Session Variables"
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