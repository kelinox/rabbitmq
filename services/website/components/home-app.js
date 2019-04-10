let template = document.createElement('template');
template.innerHTML = `
    <div>HOME</div>
`;

class HomeApp extends HTMLElement {
    constructor() {
        super();
        this._shadowRoot = this.attachShadow({ 'mode': 'open' });
        this._shadowRoot.appendChild(template.content.cloneNode(true));
    }
}

window.customElements.define('home-app', HomeApp);
export let element = document.createElement('home-app');