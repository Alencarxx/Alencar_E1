import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  

import { Observable } from 'rxjs';
import { Workitem } from './workitem'; 

@Injectable()({  
  providedIn: 'root'  
})
  
export class WorkitemService {  
  url = 'https://localhost:44353//Api/Workitem';  
  constructor(private http: HttpClient) { }  
  getAllWorkitem(): Observable<Workitem[]> {  
    return this.http.get<Workitem[]>(this.url + '/AllWorkitems');  
  }  
  getWorkitemById(employeeId: string): Observable<Workitem> {  
    return this.http.get<Workitem>(this.url + '/GetWorkitemById/' + employeeId);  
  }  
  createWorkitem(employee: Workitem): Observable<Workitem> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<Workitem>(this.url + '/InsertWorkitems/',  
    employee, httpOptions);  
  }  
  updateWorkitem(employee: Workitem): Observable<Workitem> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<Workitem>(this.url + '/UpdateWorkitems/',  
    employee, httpOptions);  
  }  
  deleteWorkitemById(employeeid: string): Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + '/DeleteWorkitems?id=' +employeeid,  
 httpOptions);  
  }  
}  