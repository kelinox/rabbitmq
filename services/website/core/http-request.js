export class HttpRequest {
    constructor(url, method, callback) {
        this.r = new XMLHttpRequest()
        this.r.open(method, url, true)
        this.callback = callback
    }

    send(data = null) {
        const state = this
        this.r.onreadystatechange = function () {
            let response = {
                success: true,
                data: null,
                errorMessage: null
            }
            if(state.r.readyState != 4 || state.r.status != 200) {
                response.success = false
                response.errorMessage = state.r.responseText
            }
            response.data = JSON.parse(state.r.responseText)
            state.callback(response)
        }
        this.r.send(data)
    }
}