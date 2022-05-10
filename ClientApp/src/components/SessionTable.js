import { useState, forwardRef, useEffect } from "react"
import MaterialTable from "material-table";
import tableIcons from "../utilities/tableIcons";

const SessionTable = ({data, onRowAdd, onRowDelete}) => {
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
                    onRowAdd: onRowAdd,
                    // onRowUpdate: (newData, oldData) =>
                    //     new Promise((resolve, reject) => {
                    //         setTimeout(() => {
                    //             const dataUpdate = [...data];
                    //             const index = oldData.tableData.id;
                    //             dataUpdate[index] = newData;
                    //             setData([...dataUpdate]);

                    //             resolve();
                    //         }, 1000);
                    //     }),
                    onRowDelete: onRowDelete
                }}
            />
        </div>
    )
}

export default SessionTable