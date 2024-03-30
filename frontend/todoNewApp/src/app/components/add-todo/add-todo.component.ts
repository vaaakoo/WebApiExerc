import { Component, EventEmitter, Output } from '@angular/core';
import { Todo } from '../../todo.model';
import { TodoService } from '../../services/todo.service';

@Component({
  selector: 'app-add-todo',
  templateUrl: './add-todo.component.html',
  styleUrl: './add-todo.component.css'
})
export class AddTodoComponent {
  newTodoText = '';

  constructor(private todoService: TodoService) { }

  addTodo(): void {
    if (!this.newTodoText.trim()) { return; }
    const newTodo: Todo = {
      id: 0, // Assuming the backend assigns IDs
      text: this.newTodoText,
      isCompleted: false
    };
    this.todoService.addTodo(newTodo).subscribe(() => {
      this.newTodoText = '';
    });
  }
}