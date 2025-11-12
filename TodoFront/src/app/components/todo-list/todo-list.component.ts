import { Component, OnInit } from '@angular/core';
import { MettecService, MettecItem } from '../../services/mettec.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  todos: MettecItem[] = [];
  newTodo: MettecItem = { title: '', description: '', isDone: false };

  constructor(private todoService: MettecService) {}

  ngOnInit() { this.loadTodos(); }

  loadTodos() { 
    this.todoService.getTodos()
      .pipe(
        catchError(err => {
          console.error('Error loading todos:', err);
          return of([]);
        })
      )
      .subscribe(data => {
        this.todos = data.sort((a, b) => Number(a.isDone) - Number(b.isDone));
      });
  }

  addTodo() {
    if (!this.newTodo.title.trim()) return;
    this.todoService.addTodo(this.newTodo).subscribe(() => {
      this.loadTodos();
      this.newTodo = { title: '', description: '', isDone: false };
    });
  }
  

  toggleStatus(todo: MettecItem) {
    todo.isDone = !todo.isDone;
    this.todoService.updateStatus(todo).subscribe(() => this.loadTodos());
  }
}
