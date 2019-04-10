import './todo-item-app.js'
import { HttpRequest } from '../core/http-request.js'

const template = document.createElement('template')
template.innerHTML = `
<style>
    :host {
        display: block;
        font-family: sans-serif;
        text-align: center;
    }

    button {
        border: none;
        cursor: pointer;
    }

    ul {
        list-style: none;
        padding: 0;
    }
</style>
<h1>To do</h1>

<input type="text" placeholder="Add a new to do"></input>
<button>+</button>

<ul id="todos"></ul>
`

class TodoApp extends HTMLElement {
    constructor() {
        super();
        this._shadowRoot = this.attachShadow({ 'mode': 'open' })
        this._shadowRoot.appendChild(template.content.cloneNode(true))

        this.$todoList = this._shadowRoot.querySelector('ul')
        this.$input = this._shadowRoot.querySelector('input')

        this.$submitButton = this._shadowRoot.querySelector('button')
        this.$submitButton.addEventListener('click', this._addTodo.bind(this))
    }

    /**
     * Every time the element is inserted into the DOM 
     * we go load the todos associated to the id
     * we pass the callback function to update the todos list once retrieve from the server
     */
    connectedCallback() {
        const httpRequest = new HttpRequest('http://localhost:8083/api/workouts', 'GET', this.updateTodos.bind(this))
        httpRequest.send()
    }

    updateTodos(todos) {
        this._todos = JSON.parse(todos).slice(0, 10)
        console.log(this._todos)
        this._renderTodos() 
    }

    _renderTodos() {
        this.$todoList.innerHTML = null;

        this._todos.forEach((todo, index) => {
            let $todoItem = document.createElement('todo-item')
            $todoItem.setAttribute('text', todo.title)

            if (todo.checked) {
                $todoItem.setAttribute('checked', '')
            }

            $todoItem.setAttribute('index', index)

            $todoItem.addEventListener('onRemove', this._removeTodo.bind(this))
            $todoItem.addEventListener('onToggle', this._toggleTodo.bind(this))

            this.$todoList.appendChild($todoItem)
        })
    }

    _addTodo() {
        if (!this._todos) this.todos = []
        if (this.$input.value.length > 0) {
            this._todos.push({ text: this.$input.value, checked: false })
            this._renderTodos()
            this.$input.value = null
        }
    }

    _removeTodo(e) {
        this._todos.splice(e.detail, 1)
        this._renderTodos()
    }

    _toggleTodo(e) {
        const index = e.detail
        const todo = this._todos[index]
        this._todos[index] = Object.assign({}, todo, {
            checked: !todo.checked
        })
        this._renderTodos()
    }
}

window.customElements.define('todo-app', TodoApp)
export const element = document.createElement('todo-app')