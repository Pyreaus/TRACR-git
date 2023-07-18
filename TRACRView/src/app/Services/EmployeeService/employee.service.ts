import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddModifyEmpReq } from 'src/app/Interfaces/DTOs/AddModifyEmpReq';
import { Employee } from 'src/app/Interfaces/employee';
import { env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  staticEmployees$!: Employee[];
  constructor(private http: HttpClient) {}

  GetEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${env.ApiUrl}/${env.version}/${env.empController}/GetEmployees`);
  }
  EditEmployee(id: string, modifyEmployeeRequest: AddModifyEmpReq): Observable<Employee> {
    return this.http.put<Employee>(`${env.ApiUrl}/${env.version}/${env.empController}/EditEmployee/${id}`,modifyEmployeeRequest);
  }
  GetEmployee(id: string): Observable<Employee> {
    return this.http.get<Employee>(`${env.ApiUrl}/${env.version}/${env.empController}/GetEmployee/${id}`);
  }
  AddEmployee(addEmployeeRequest: AddModifyEmpReq): Observable<Employee> {
    return this.http.post<Employee>(`${env.ApiUrl}/${env.version}/${env.empController}/AddEmployee`,addEmployeeRequest);
  }
  DeleteEmployee(id: string): Observable<Employee> {
    return this.http.delete<Employee>(`${env.ApiUrl}/${env.version}/${env.empController}/DeleteEmployee/${id}`);
  }

  GetEmployeesStatic(): Promise<Employee[]> {
    this.staticEmployees$ = [
      {
        id: '1',
        name: 'Noel',
        email: 'noel.ar@protonmail.com',
        phone: '03453234'
      },
      {
        id: '2',
        name: 'Bob',
        email: 'noel.ar@protonmail.com',
        phone: '03453234'
      },
      {
        id: '3',
        name: 'Dan',
        email: 'noel.ar@protonmail.com',
        phone: '03453234'
      },
      {
        id: '4',
        name: 'Stephen',
        email: 'noel.ar@protonmail.com',
        phone: '03453234'
      }
    ];
    return Promise.resolve(this.staticEmployees$);
  }

}
