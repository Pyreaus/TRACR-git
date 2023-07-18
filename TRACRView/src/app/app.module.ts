import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { ScrollerModule } from 'primeng/scroller';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropdownModule } from 'primeng/dropdown';
import { SidebarModule } from 'primeng/sidebar';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { CalendarModule } from 'primeng/calendar';
import { FieldsetModule } from 'primeng/fieldset';
import { PanelModule } from 'primeng/panel';
import { MultiSelectModule } from 'primeng/multiselect';
import { CardModule } from 'primeng/card';
import { MessagesModule } from 'primeng/messages';
import { TooltipModule } from 'primeng/tooltip';
import { SelectButtonModule } from 'primeng/selectbutton';
import { AppComponent } from './Components/app.component';
import { HOMEComponent } from './Components/HOME/home.component';
import { NAVBARComponent } from './Components/NAVBAR/navbar.component';
import { NOTFOUNDComponent } from './Components/404/notfound.component';
import { EmployeeComponent } from './Components/dev-maintenance/employee.component';
import { EmployeeAddEditComponent } from './Components/dev-maintenance/employee-add-edit/employee-add-edit.component';
import { EmployeeDeleteComponent } from './Components/dev-maintenance/employee-delete/employee-delete.component';
import { NewDiaryTaskComponent } from './Components/HOME/new-diarytask/new-diarytask';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//services imports:
import { EmployeeService } from './Services/EmployeeService/employee.service';
import { UserService } from './Services/UserService/user.service';
//pipe imports:
import { FontSizePipe } from './Pipes/font-size.pipe';
import { FilterPipe } from './Pipes/filter.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HOMEComponent,
    NAVBARComponent,
    NOTFOUNDComponent,
    EmployeeComponent,
    EmployeeAddEditComponent,
    FilterPipe,
    FontSizePipe,
    EmployeeDeleteComponent,
    NewDiaryTaskComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MultiSelectModule,
    HttpClientModule,
    FormsModule,
    DropdownModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    SidebarModule,
    ButtonModule,
    ScrollerModule,
    PaginatorModule,
    TooltipModule,
    DividerModule,
    MessagesModule,
    TableModule,
    CalendarModule,
    CardModule,
    PanelModule,
    FieldsetModule,
    SelectButtonModule
  ],
  providers: [
    UserService,
    EmployeeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
