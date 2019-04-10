let template = document.createElement('template');
template.innerHTML = `
    <div>Workouts module</div>
`;

class WorkoutsApp extends HTMLElement {
    constructor() {
        super();
        this._shadowRoot = this.attachShadow({ 'mode': 'open' });
        this._shadowRoot.appendChild(template.content.cloneNode(true));
    }

    get parameter() {
        return this._parameter
    }

    set parameter(value) {
        this._parameter = value
        this._fetchWorkouts()
    }

    _fetchWorkouts() {
    }
}

window.customElements.define('workout-app', WorkoutsApp);
export let element = document.createElement('workout-app');