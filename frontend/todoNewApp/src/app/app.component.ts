import { Component } from '@angular/core';
import { Todo } from './todo.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  todos: Todo[] = [];

  onTodoAdded(todo: Todo) {
    this.todos.push(todo);
  }

  onTodoDeleted(id: number) {
    this.todos = this.todos.filter(todo => todo.id !== id);
  }
}