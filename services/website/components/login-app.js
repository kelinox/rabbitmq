import { HttpRequest } from '../core/http-request.js'

const template = document.createElement('template')
template.innerHTML = `
<style>
    @import "../bootstrap/css/bootstrap.min.css"
</style>

<style>
    .not-valid {
        border: 1px solid red;
    }
</style>

<div class="container mt-5">
    <div class="row justify-content-md-center">
        <div class="col-md-auto">
            <form class="form-signin" action="">
                <h1 class="h3 mb-3 font-weight-normal">Please sign in</h1>
                <input type="text" id="username_input" class="form-control" placeholder="Username" required>
                <input type="password" id="password_input" class="form-control" placeholder="Password" required>
                <div class="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me"> Remember me
                    </label>
                </div>
                <button class="btn btn-lg btn-primary btn-block" type="submit">Login</button>
            </form>
        </div>
    </div>
</div>
`

class LoginApp extends HTMLElement {

    constructor() {
        super()
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        this.$username = this._shadowRoot.querySelector('#username_input')
        this.$password = this._shadowRoot.querySelector('#password_input')

        this.$button = this._shadowRoot.querySelector('button')
        this.$button.addEventListener('click', this._login.bind(this))

    }

    /**
     * Function called when the user submit the form, it gets the username and password 
     * Then call the api to authenticate the user
     */
    _login() {
        const username = this.$username.value
        const password = this.$password.value

        const http = new HttpRequest('http://localhost:8081/api/login', 'POST', this._logged.bind(this))
        http.send({'username': username, 'password': password})
    }

    /**
     * Callback function called when the HttpRequest is finished
     * Do the check to see if the user is well authenticated or not
     * If not display error message
     * If authenticated store the access token
     * @param {boolean} success if the call to the API succeed
     * @param {string} data the jwt token returned by the API
     * @param {string} errorMessage the error message if the API call failed 
     */
    _logged({success, data, errorMessage}) {
        if(response.success) {
            localStorage.setItem('access_token', jwtToken)
        } else {
            console.error(response.errorMessage)
        }
    }
}

export default LoginApp