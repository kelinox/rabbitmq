import { LitElement, html } from 'lit-element'

const template = document.createElement('template')
template.innerHTML = `

`

class WorkoutsList extends LitElement {
    constructor() {
        super()
        this.shadowRoot = this.attachShadow({ 'mode': 'open' })
    }
}

customElements.define('workouts-list', WorkoutsList)