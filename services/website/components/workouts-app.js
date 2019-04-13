import { HttpRequest } from '../core/http-request.js'
import './workouts-list.js'

const template = document.createElement('template')
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
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        this.$list = this.shadowRoot.querySelector('#workouts-list')
    }

    connectedCallback() {
    }

    get parameter() {
        return this._parameter
    }

    set parameter(value) {
        console.log('set')
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
    }

    _fetchWorkouts() {
        // const httpRequest = new HttpRequest('http://localhost:8083/api/workouts', 'GET', this.updateWorkouts.bind(this))
        // httpRequest.send()
    }

    /**
     * 
     * @param {boolean} success if the call to the API succeed
     * @param {string} data the jwt token returned by the API
     * @param {string} errorMessage the error message if the API call failed 
     */
    updateWorkouts({success, data, errorMessage}) {
        if(success) {
            let $workoutList = document.createElement('workouts-list')
            this.$list.appendChild($workoutList)
            this.workouts = data
        } else {
            console.error(errorMessage)
        }
        
    }

}

export default WorkoutsApp