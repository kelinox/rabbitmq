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
            contentUri = 'error-app'
        }
        this._addLinkImport(contentUri)
    }

    /**
     * We dynamically remove the components present in the #app element
     * And then we load the new component 
     * @param {string} uri 
     */
    async _addLinkImport(uri) {
        const previousElement = document.querySelector('#dynamic-component')
        if (previousElement) {
            this.app.removeChild(previousElement)
        }

        const route = this._getRoute(uri)

        if (route.route) {
            this._importContent(route)
        }
    }

    /**
     * We dynamically import the component associated to the route
     * If the route has parameter, we set the value of the element's parameter
     * @param {any} route 
     */
    _importContent(route) {
        const path = `./components/${route.route.component}.js`
        import(path)
            .then((module) => {
                var element = module.element
                element.setAttribute('id', 'dynamic-component')
                if (route.hasParameter) {
                    element.parameter = route
                }
                this.app.appendChild(element)
            })
            .catch((e) => {
                var errorDiv = document.createElement('div')
                errorDiv.setAttribute('id', 'dynamic-component')
                errorDiv.innerText = e.toString()
                this.app.appendChild(errorDiv)
            })
    }

    /**
     * Get the route define in the routes.js file from a URI
     * If the URI has a parameter then we look every routes that have a parameter
     * Otherwise we look only the routes that do not have one
     * @param {string} uri 
     */
    _getRoute(uri) {
        let route = {
            route: null,
            hasParameter: false,
            parameter: null
        };
        if (this._uriHasParameters(uri)) {
            route.hasParameter = true

            const path = uri.split('/')[0]

            route.route = routes.find((element) => {
                return /(\/:id)$/.test(element.path) && element.path.split('/:id')[0] === path
            })

            if (route.route) {
                route.parameter = this._getUriParameter(uri)
            }

        } else {
            route.route = routes.find((element) => {
                return element.path === uri
            })
        }
        return route
    }

    /**
     * Get parameter from a URI of type [controller]/[parameter]/[detail]
     * We split the string by / and then get the second item which will always be the parameter
     * Where /[detail] is optionnal
     * @param {string} uri 
     */
    _getUriParameter(uri) {
        return uri.split('/')[1]
    }

    /**
     * Check if the URI has a parameter
     * Every uri like [controller]/[parameter]/[detail] are valid
     * Where /[detail] is optionnal
     * @param {string} uri 
     */
    _uriHasParameters(uri) {
        return /^[a-z]{1,}\/[0-9]{1,}(\/[a-z]{1,})?$/.test(uri)
    }

    /**
     * Check if a URI is valid
     * Here we only accept as valid, a URI like [controller]/[parameter]/[detail]
     * Where /[detail] is optionnal
     * @param {string} uri 
     */
    _isValidUri(uri) {
        return /[a-z]{1,}(\/[0-9]{1,})?(\/[a-z]{1,})?/.test(uri)
    }
}

const router = new Router()