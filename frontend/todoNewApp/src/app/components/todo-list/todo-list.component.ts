import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Todo } from '../../todo.model';
import { TodoService } from '../../services/todo.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css'
})

export class TodoListComponent implements OnInit {
  todos: Todo[] = [];
  filteredTodos: Todo[] = [];
  selectedTodo: Todo | undefined;

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.todoService.getTodos().subscribe(todos => {
      this.todos = todos;
      this.filteredTodos = todos; // Initialize filteredTodos with all todos
    });
  }

  deleteTodo(id: number): void {
    this.todoService.deleteTodo(id).subscribe(() => {
      this.todos = this.todos.filter(todo => todo.id !== id);
      this.filteredTodos = this.filteredTodos.filter(todo => todo.id !== id); // Update filteredTodos as well
    });
  }

  updateTodo(todo: Todo): void {
    this.todoService.updateTodo(todo).subscribe();
  }

  filterTodos(event: { query: any; }): void {
    const query = event.query;
    this.filteredTodos = this.todos.filter(todo => todo.text.toLowerCase().includes(query.toLowerCase()));
  }
}