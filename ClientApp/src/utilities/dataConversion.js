const convertToArray = dictionary => 
    [...Object.keys(dictionary).map(key => ({ key: key, value: dictionary[key] }))]

const convertToDictionary = array => 
    Object.assign({}, ...array.map(element => ({[element.key]: element.value})))

export { convertToArray, convertToDictionary }