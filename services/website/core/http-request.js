export class HttpRequest {
    constructor(url, method, callback) {
        this.r = new XMLHttpRequest()
        this.r.open(method, url, true)
        this.callback = callback
        this.r.setRequestHeader('Content-Type', 'application/json')
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
                response.errorMessage = state.r.statusText          
            }
            if(response.success) {
                console.log('success')
                console.log(response)
                response.data = JSON.parse(state.r.responseText)
            }
            state.callback(response)
        }
        console.log(data)
        this.r.send(JSON.stringify(data))
    }
}