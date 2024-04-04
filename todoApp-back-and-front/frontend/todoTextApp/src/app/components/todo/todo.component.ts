import { Component, OnInit } from '@angular/core';
import { Todo } from '../../model/todoText';
import { TodoService } from '../../services/todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {
  todos: Todo[] = [];
  filteredTodos: Todo[] = [];
  newTodo: Todo = { id: 0, title: '', isCompleted: 0 };
  editingTodoId: number | null = null;
  searchTerm: string = '';

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.todoService.getTodos()
      .subscribe(todos => {
        this.todos = todos;
        this.filteredTodos = todos;
        console.log('Todos:', todos); 
      });
  }

  addTodo(): void {
    this.todoService.addTodo(this.newTodo)
      .subscribe(todo => {
        this.todos.push(todo);
        this.filteredTodos.push(todo);
        this.newTodo = { id: 0, title: '', isCompleted: 0 };
      });
  }

  editTodo(id: number): void {
    this.editingTodoId = id;
  }

  saveTodoChanges(todo: Todo): void {
    const updatedTodo = this.todos.find(t => t.id === todo.id);
    if (updatedTodo) {
      updatedTodo.title = todo.title;
      updatedTodo.isCompleted = todo.isCompleted;
      this.todoService.updateTodo(updatedTodo.id, updatedTodo) // Pass updatedTodo here
        .subscribe(() => this.editingTodoId = null);
    }
  }

  cancelEdit(): void {
    this.editingTodoId = null;
  }

  deleteTodo(id: number): void {
    this.todoService.deleteTodo(id)
      .subscribe(() => {
        this.todos = this.todos.filter(todo => todo.id !== id);
        this.filteredTodos = this.filteredTodos.filter(todo => todo.id !== id);
      });
  }

  filterTodos(event: any): void {
  if (this.searchTerm.trim() === '') {
    this.filteredTodos = this.todos;
  } else {
    this.filteredTodos = this.todos.filter(todo => todo.title.toLowerCase().includes(this.searchTerm.toLowerCase()));
  }
}

  search(): void {
    if (this.searchTerm.trim() === '') {
      this.filteredTodos = this.todos;
    } else {
      this.filteredTodos = this.todos.filter(todo => todo.title.toLowerCase().includes(this.searchTerm.toLowerCase()));
    }
  }
}
