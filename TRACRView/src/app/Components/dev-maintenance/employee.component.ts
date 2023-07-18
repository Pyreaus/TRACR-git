import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/Interfaces/employee';
import { EmployeeService } from 'src/app/Services/EmployeeService/employee.service';
import { FilterPipe } from 'src/app/Pipes/filter.pipe';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styles: []
})
export class EmployeeComponent implements OnInit {
  employees$!: Observable<Employee[]>;
  selectedEmp!: Employee;
  modalOpen: boolean = false;
  idFil!: string;
  nameFil!: string;
  entity!: string;

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    [this.idFil, this.nameFil, this.entity] = ['', '', 'Employee'];
    this.employees$ = this.employeeService.GetEmployees();
    console.log(this.employees$);
  }

  openDeleteModal(employee: Employee) {
    [this.selectedEmp, this.modalOpen] = [employee,true];
  }

  closeDeleteModal(id: string) {
    this.employeeService.DeleteEmployee(id).subscribe(_ => this.employees$ = this.employeeService.GetEmployees());
    console.info('Employee deleted');
  }
}
