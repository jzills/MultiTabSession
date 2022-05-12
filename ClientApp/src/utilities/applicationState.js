import { sessionHeaders } from "./sessionHeaders"
import { convertToDictionary } from "./dataConversion"

const batch = async applicationState => {
    const response = await fetch("session/batchupdateapplicationstate", {
        method: "post",
        headers: { 
            "content-type": "application/json",
            [sessionHeaders.SESSION]: window.name 
        },
        body: JSON.stringify(convertToDictionary(applicationState))
    })

    return response.ok
}

export default batch