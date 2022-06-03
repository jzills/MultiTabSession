import batch from "../utilities/applicationState"

const TIMEOUT = 1000

const useTable = refresh => {

    const onRowAdd = applicationState => data => 
        handleTimeout(handleBatch([...applicationState, data]))

    const onRowUpdate = applicationState => (data, prevData) =>
        handleTimeout((() => {
            const temp = [...applicationState]
            temp[prevData.tableData.id] = data
            return handleBatch(temp)
        })())

    const onRowDelete = applicationState => data => 
        handleTimeout((() => {
            const temp = [...applicationState]
            temp.splice(data.tableData.id, 1)
            return handleBatch(temp)
        })())

    const handleBatch = data => async (resolve, reject) => {
        if (await batch(data)) {  
            resolve(await refresh())
        } else reject()
    }

    const handleTimeout = callback => 
        new Promise((resolve, reject) => {
            setTimeout(async () => await callback(resolve, reject), TIMEOUT)
        })

    return [onRowAdd, onRowUpdate, onRowDelete]
}

export default useTable