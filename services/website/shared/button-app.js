//#region Template

const template = document.createElement('template')
template.innerHTML = `
<style>
    button {
        padding: 15px;
        padding-left: 65px;
        padding-right: 65px;
        flex: 1;  
        border: none;
        border-radius: 5px;  
        cursor: pointer;
    }
</style>
<button type="submit"></button>
`

//#endregion

class ButtonApp extends HTMLElement {

    constructor() {
        super()
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        this.$button = this._shadowRoot.querySelector('button')

        this.$button.addEventListener('click', (e) => {
            this.dispatchEvent(new CustomEvent('onLogin'))
        })
    }

    static get observedAttributes() {
        return ['text', 'color', 'background']
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case 'text':
                this.$button.innerText = newValue
                break
            case 'color':
                this.$button.style.color = newValue
                break
            case 'background':
                this.$button.style.backgroundColor = newValue
                this.$button.style.boxShadow = `1px 1px 3px 2px ${newValue}`
                break
            default:
                break
        }
    }
}

window.customElements.define('button-app', ButtonApp)