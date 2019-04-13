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

export default HomeApp