import '../shared/button-app.js'

const template = document.createElement('template')
template.innerHTML = `
<script>

</script>

<div>
    <form>
        <label>Username</label>
        <input type="text"/>
        <label>Email</label>
        <input type="email"/>
        <label>Password</label>
        <input type="password"/>
    </form>
</div>

`

class RegisterApp extends HTMLElement {
    constructor() {
        super()
        this._shadowRoot = this.attachShadow({'mode': 'open'})
        this._shadowRoot.appendChild(template.content.cloneNode(true))
    }
}