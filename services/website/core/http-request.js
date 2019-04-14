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
                errorMessage: ''
            }
            if (state.r.readyState == 4) {
                if (state.r.status != 200) {
                    state._handleError(state.r, response)
                }
                else {
                    response.data = JSON.parse(this.r.responseText)
                }
            }
            state.callback(response)
        }
        this.r.send(JSON.stringify(data))
    }

    _handleError(request, response) {
        response.success = false
        switch (request.status) {
            case 0:
                response.errorMessage = "Server is not responding, try again later"
                break;        
            default:
                break;
        }
    }
}