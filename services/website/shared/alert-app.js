const template = document.createElement('template')
template.innerHTML = `
<style>
    .alert {
        padding: .75rem 1.25rem;
        border-radius: 7px;
        max-width:100%;
        margin-bottom: 50px;
    }
</style>
<div class="alert">

</div>
`

class AlertApp extends HTMLElement {
    constructor() {
        super()
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        this.$alert = this._shadowRoot.querySelector('.alert')
    }

    static get observedAttributes() {
        return ['color', 'background', 'text', 'display']
    }

    get show() {
        return this._show
    }

    set show(value) {
        this._show = value
        this._updateDisplay()
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case 'text':
                this.$alert.innerText = newValue
                break;
            case 'color':
                this.$alert.style.color = newValue
                break
            case 'background':
                this.$alert.style.backgroundColor = newValue
                break
            case 'display':
                console.log(newValue)
                this.$alert.style.display = newValue ? newValue : null
                break
            default:
                break;
        }
    }

    _updateDisplay() {
        this.$alert.style.visibility = this._show ? 'visible' : 'hidden'
    }
}

window.customElements.define('alert-app', AlertApp)