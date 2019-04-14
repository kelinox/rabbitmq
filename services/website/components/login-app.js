import { HttpRequest } from '../core/http-request.js'
import '../shared/button-app.js'

//#region Template
const template = document.createElement('template')
template.innerHTML = `
<style>
    .container-login {
        width:400px;
        margin:auto;
    }

    .flex {
        display: flex;
        margin-top: 40px;
        align-items: center;
    }

    .flex-child {
        flex: 1;
    }

    label {
        display: block;
        margin-bottom: 10px;
        font-weight:700;
    }

    input {
        border: none;
        border-bottom: 2px solid #f5f5f5;
        width:100%;
        margin-bottom:20px;
    }

    input:focus {
        outline:none;
    }

    #error {
        display: none;
    }

    .display {
        display: block !important;
    }

    a {
        color: #d4d4d4;
    }

</style>

<div class="container-login">
    <form class="form-signin" action="">
        <div id="error"></div>
        <label for="username_input">Username</label>
        <input type="text" id="username_input" class="form-control" required>
        <label for="password_input">Password</label>
        <input type="password" id="password_input" class="form-control" required>
        <div class="flex">
            <a class="flex-child" href="">Forget password</a>
        </div>
    </form>
</div>
`
//#endregion

//#region Class
class LoginApp extends HTMLElement {

    constructor() {
        super()
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        this.$username = this._shadowRoot.querySelector('#username_input')
        this.$password = this._shadowRoot.querySelector('#password_input')

        this.$flex = this._shadowRoot.querySelector('.flex')

        this.$button = document.createElement('button-app')
        this.$button.setAttribute('color', 'white')
        this.$button.setAttribute('background', '#3178f8')
        this.$button.setAttribute('text', 'LOGIN')

        this.$button.addEventListener('onLogin', (e) => this._login(e))

        this.$flex.appendChild(this.$button)

        this.$error = this._shadowRoot.querySelector('#error')

    }

    /**
     * Function called when the user submit the form, it gets the username and password 
     * 
     * Then call the api to authenticate the user
     * @param {event} e event of the action, allow not to refresh the page on submit
     */
    _login(e) {
        e.preventDefault()
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
        if(success) {
            console.log(data)
            localStorage.setItem('access_token', data)
            window.location= '#home'
        } else {
            this.$error.classList.add('display')
            if(errorMessage.Length > 0){
                this.$error.innerHTML = errorMessage
            } else {
                this.$error.innerHTML = "Server is not responding, try later"
            }
        }
    }
}
//#endregion

export default LoginApp