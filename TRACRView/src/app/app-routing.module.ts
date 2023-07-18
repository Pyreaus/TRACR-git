import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HOMEComponent } from './Components/HOME/home.component';
import { NOTFOUNDComponent } from './Components/404/notfound.component';
import { EmployeeAddEditComponent } from './Components/dev-maintenance/employee-add-edit/employee-add-edit.component';
import { EmployeeComponent } from './Components/dev-maintenance/employee.component';
import { MimeTypeResolver } from './Shared/mime-type-resolver';

const routes: Routes = [
  { path: 'home', component: HOMEComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'employee/add/:id', component: EmployeeAddEditComponent },
  { path: 'employee/edit/:id', component: EmployeeAddEditComponent },
  { path: 'employees', component: EmployeeComponent },
  { path: '404', component: NOTFOUNDComponent },
  { path: '**', redirectTo: '/404', pathMatch: 'full', resolve:
    {mime: MimeTypeResolver}, data: {headers: new Headers({ 'X-Content-Type-Options': 'nosniff' })}
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
