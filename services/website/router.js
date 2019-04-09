import { routes } from './routing/routes.js'

class Router {
    constructor() {
        window.addEventListener('hashchange', (e) => this.onRouteChange(e))
        this.app = document.querySelector('#app')
        this.onRouteChange()
    }

    onRouteChange(e) {
        const hashString = window.location.hash.substring(1)
        this.loadContent(hashString)
    }

    loadContent(uri) {
        let contentUri;
        if (this._isValidUri(uri)) {
            contentUri = uri
        } else {
            console.log('not valid')
            contentUri = 'error-app'
        }
        this._addLinkImport(contentUri)
    }

    async _addLinkImport(contentUri) {
        const previousElement = document.querySelector('#dynamic-component')
        if (previousElement) {
            this.app.removeChild(previousElement)
        }

        const route = routes.find((e) => {
            return e.path.substring(1) === contentUri
        })

        if (route) {
            const path = `./js/${route.component}.js`
            import(path)
            .then((module) => {
                var element = module.element
                element.setAttribute('id', 'dynamic-component')
                this.app.appendChild(element)
            })
            .catch((e) => {
                var errorDiv = document.createElement('div')
                errorDiv.setAttribute('id', 'dynamic-component')
                errorDiv.innerText = e.toString()
                this.app.appendChild(errorDiv)
            })
        }
    }

    _isValidUri(uri) {
        return /[a-z]{1,}(\/[0-9]{1,})?/.test(uri)
    }
}

const router = new Router()