import { HttpRequest } from '../core/http-request.js'
import './workouts-list.js'

const template = document.createElement('template');

template.innerHTML = `
    <style>
        @import "../bootstrap/css/bootstrap.min.css"
    </style>
    <div>Workouts module</div>
    <div id="workouts-list"></div>
`;

class WorkoutsApp extends HTMLElement {
    constructor() {
        super();
        this._shadowRoot = this.attachShadow({ 'mode': 'open' });
        this._shadowRoot.appendChild(template.content.cloneNode(true));

        this.$list = this.shadowRoot.querySelector('#workouts-list')
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
        // const httpRequest = new HttpRequest('http://localhost:8083/api/workouts', 'GET', this.updateWorkouts.bind(this))
        // httpRequest.send()
        this.updateWorkouts([])
    }

    updateWorkouts(value) {
        let $workoutList = document.createElement('workouts-list')
        this.$list.appendChild($workoutList)
        this.workouts = value
    }

}

window.customElements.define('workout-app', WorkoutsApp);
export let element = document.createElement('workout-app');