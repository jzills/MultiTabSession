const convertToArray = dictionary => 
    [...Object.keys(dictionary).map(key => ({ key: key, value: dictionary[key] }))]

const convertToDictionary = array => 
    Object.assign({}, ...array.map(element => ({[element.key]: element.value})))

const convertToWords = string => {
    let words = string.replace(/([A-Z])/g, " $1")
    return `${words[0].toUpperCase()}${words.substring(1)}`
}

export { convertToArray, convertToDictionary, convertToWords }