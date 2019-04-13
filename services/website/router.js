import { routes } from './routing/routes.js'

class Router {
    constructor() {
        window.addEventListener('hashchange', (e) => this.onRouteChange(e))
        this.app = document.querySelector('#app')
        this.onRouteChange()
    }

    /**
     * Method called when the url changed
     */
    onRouteChange() {
        this.loadContent(window.location.hash.substring(1))
    }

    /**
     * Method that load the content of the page dynamicly 
     * @param {string} uri 
     */
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
    _importContent({ route, hasParameter, parameter }) {
        const path = `./components/${route.component}.js`
        import(path)
            .then((module) => {
                if (!this._isDefined(route.component)) {
                    window.customElements.define(route.component, module.default)
                }
                const element = document.createElement(route.component)
                element.setAttribute('id', 'dynamic-component')
                if (hasParameter) {
                    element.parameter = parameter
                }
                this.app.appendChild(element)
            })
            .catch((e) => {
                console.error(e)
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
 
            route.route = this._getRouteFromConfig(uri)

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
     * We split the string by '/' and then get the second item which will always be the parameter
     * Where /[detail] is optionnal
     * @param {string} uri 
     * @return {string} the parameter of the URI 
     */
    _getUriParameter(uri) {
        return uri.split('/')[1]
    }

    /**
     * Check if the URI has a parameter
     * Every URI like [controller]/[parameter]/[detail] are valid
     * Where /[detail] is optionnal
     * @param {string} uri 
     * @return {boolean} true if the URI has a parameter
     */
    _uriHasParameters(uri) {
        return /^[a-z]{1,}\/[0-9]{1,}(\/[a-z]{1,})?$/.test(uri)
    }

    /**
     * Check if a URI is valid
     * Here we only accept as valid, a URI like [controller]/[parameter]/[detail]
     * Where /[detail] is optionnal
     * @param {string} uri 
     * @return {boolean} true if the URI valid
     */
    _isValidUri(uri) {
        return /[a-z]{1,}(\/[0-9]{1,})?(\/[a-z]{1,})?/.test(uri)
    }

    /**
     * Check if a custom element has already been defined
     * @param {string} elementName element to check
     * @returns {boolean} true if already defined, false if not
     */
    _isDefined(elementName) {
        return window.customElements.get(elementName) !== undefined
    }

    /**
     * We get the route, from the routes.js file, that match with the URI
     * @param {string} uri The URI with the parameter
     * @return {any} return the route 
     */
    _getRouteFromConfig(uri) {
        const path = uri.split('/')[0]
        return routes.find((element) => {
            return /(\/:id)$/.test(element.path) && element.path.split('/:id')[0] === path
        })

    }
}

const router = new Router()