import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'


export interface MettecItem {
    id?: number;
    title: string;
    description: string;
    isDone: boolean;
}

@Injectable({ providedIn: 'root' })
export class MettecService {
    private apiUrl = environment.apiUrl + '/api/mettec';

    constructor(private http: HttpClient) {}

    getTodos(): Observable<MettecItem[]> { 
        return this.http.get<MettecItem[]>(this.apiUrl); 
    }

    addTodo(todo: MettecItem): Observable<MettecItem> { 
        return this.http.post<MettecItem>(this.apiUrl, todo); 
    }

    updateStatus(todo: MettecItem): Observable<void> { 
        return this.http.put<void>(`${this.apiUrl}/${todo.id}`, todo); 
    }

    deleteTodo(id: number): Observable<void> { 
        return this.http.delete<void>(`${this.apiUrl}/${id}`); 
    }
}