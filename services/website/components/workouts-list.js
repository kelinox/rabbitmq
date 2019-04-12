const template = document.createElement('template')
template.innerHTML = `
    <style>
        @import "../bootstrap/css/bootstrap.min.css"
    </style>
    <div class="alert alert-primary" role="alert">
        A simple primary alert—check it out!
    </div>
`

class WorkoutsList extends HTMLElement {
    constructor() {
        super()
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        console.log('end constructor')
    }
}

customElements.define('workouts-list', WorkoutsList)