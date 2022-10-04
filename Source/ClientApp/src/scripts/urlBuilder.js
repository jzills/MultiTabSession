class UrlBuilder {
    constructor({ base, path, query, queryArg }) {
        this.base = base
        this.path = path
        this.query = query
        this.queryArg = queryArg
    }

    build = () => `${this.base}/${this.path}?${this.query}=${this.queryArg}`
}

export default UrlBuilder