import { Component, Input } from '@angular/core';
import { Employee } from 'src/app/Interfaces/employee';

@Component({
  selector: 'employee-delete',
  template: `<h5 style="white-space: nowrap;">Are you sure you want to remove
  <span [style.color]="'red'"><strong>{{ employee?.name }}</strong></span>?</h5>
  <br><h6>ID: <span [style]="employee?.id | fontSize : '12px'">{{ employee?.id }}</span></h6>`,
  styles: [
  ]
})
export class EmployeeDeleteComponent {
  @Input() employee!: Employee;

}

