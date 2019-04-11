import { HttpRequest } from '../core/http-request.js'
const template = document.createElement('template');

template.innerHTML = `
    <div>Workouts module</div>
`;

class WorkoutsApp extends HTMLElement {
    constructor() {
        super();
        this._shadowRoot = this.attachShadow({ 'mode': 'open' });
        this._shadowRoot.appendChild(template.content.cloneNode(true));
    }

    connectedCallback() {
    }

    get parameter() {
        return this._parameter
    }

    set parameter(value) {
        this._parameter = value
        this._fetchWorkouts()
    }

    get workouts() {
        return this._workouts
    }

    set workouts(value) {
        this._workouts = value
        this._updateDisplay()
    }

    _updateDisplay() {
        console.log('display')
    }

    _fetchWorkouts() {
        const httpRequest = new HttpRequest('http://localhost:8083/api/workouts', 'GET', this.updateWorkouts.bind(this))
        httpRequest.send()
    }

    updateWorkouts(value) {
        this.workouts = value
    }

}

window.customElements.define('workout-app', WorkoutsApp);
export let element = document.createElement('workout-app');