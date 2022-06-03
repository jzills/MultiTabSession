import MaterialTable from "material-table"
import tableIcons from "../utilities/tableIcons"
import useTable from "../hooks/useTable"

const SessionTable = ({data, header, refresh}) => {
    const [onRowAdd, onRowUpdate, onRowDelete] = useTable(refresh)

    return (
        <div className={"session-table"}>
            <h4>{header}</h4>
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