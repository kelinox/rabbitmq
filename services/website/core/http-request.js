export class HttpRequest {
    constructor(url, method, callback) {
        this.r = new XMLHttpRequest()
        this.r.open(method, url, true)
        this.callback = callback
    }

    send(data = null) {
        const state = this
        this.r.onreadystatechange = function () {
            if(state.r.readyState != 4 || state.r.status != 200) return
            console.log(state.r.responseText)
            state.callback(state.r.responseText)
        }
        this.r.send(data)
    }
}