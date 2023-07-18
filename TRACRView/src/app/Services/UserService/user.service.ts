import { User } from 'src/app/Interfaces/User';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Trainee } from 'src/app/Interfaces/Trainee';
import { env } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { AddModifyTraineeReq } from 'src/app/Interfaces/DTOs/AddModifyTraineeReq';
import { AddModifyDiaryReq } from 'src/app/Interfaces/DTOs/AddModifyDiaryReq';
import { Diary } from 'src/app/Interfaces/Diary';
import { DiaryTask } from 'src/app/Interfaces/DiaryTask';
import { Skill } from 'src/app/Interfaces/Skill';
import { AddModifyTaskReq } from 'src/app/Interfaces/DTOs/AddModifyTaskReq';

@Injectable({
  providedIn: 'root'
}) export class UserService {

  staticTrainees$!: Trainee[];
  constructor(private http: HttpClient) {}

  GetUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetUsers`)
  }
  SetPair(PfId: number, addReq: AddModifyTraineeReq): Observable<AddModifyTraineeReq> {
    return this.http.put<AddModifyTraineeReq>(`${env.ApiUrl}/${env.version}/${env.usrController}/SetPair/${PfId}`, addReq);
  }
  EditDiaryPfid(PfId: number, addDiaryRequest: AddModifyDiaryReq): Observable<AddModifyDiaryReq> {
    return this.http.put<AddModifyDiaryReq>(`${env.ApiUrl}/${env.version}/${env.diaryController}/EditDiaryPfid/${PfId}`, addDiaryRequest);
  }
  getTraineesByReviewer(PfId: number): Observable<Trainee[]> {
    return this.http.get<Trainee[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetTraineesByReviewer/${PfId}`);
  }
  AssignTrainees(PfId: number, addReq: AddModifyTraineeReq): Observable<AddModifyTraineeReq> {
    return this.http.post<AddModifyTraineeReq>(`${env.ApiUrl}/${env.version}/${env.usrController}/AssignTrainees/${PfId}`, addReq);
  }
  GetTrainees(): Observable<Trainee[]> {
    return this.http.get<Trainee[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetTrainees`);
  }
  GetReviewers(): Observable<User[]> {
    return this.http.get<User[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetReviewers`);
  }
  GetTrainee(PfId: number): Observable<Trainee> {
    return this.http.get<Trainee>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetTrainee/${PfId}`);
  }
  getUserType(): Observable<User> {
    return this.http.get<User>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetUserType`);
  }
  GetTasksDiaryId(DiaryId: number): Observable<DiaryTask[]> {
    return this.http.get<DiaryTask[]>(`${env.ApiUrl}/${env.version}/${env.diaryController}/GetTasksByDiaryId/${DiaryId}`)
  }
  AddDiary(addDiaryRequest: AddModifyDiaryReq): Observable<AddModifyDiaryReq> {
    return this.http.post<AddModifyDiaryReq>(`${env.ServerRoot}/api/${env.version}/${env.diaryController}/AddDiary`,addDiaryRequest);
  }
  AddDiaryTask(addDiaryTaskRequest: AddModifyTaskReq): Observable<AddModifyTaskReq> {
    return this.http.post<AddModifyTaskReq>(`${env.ApiUrl}/${env.version}/${env.diaryController}/AddDiaryTask`,addDiaryTaskRequest);
  }
  GetDiaryPfid(PfId: number): Observable<Diary> {
    return this.http.get<Diary>(`${env.ApiUrl}/${env.version}/${env.diaryController}/GetDiaryPfid/${PfId}`);
  }
  // GetDiary(id: string): Observable<Employee> {
  //   return this.http.get<Employee>(
  //     `${env.ServerRoot}/api/${env.version}/${env.controller}/GetEmployee/${id}`);
  // }
  // AddDiary(addEmployeeRequest: AddModifyEmpReq): Observable<Employee> {
  //   return this.http.post<Employee>(
  //     `${env.ServerRoot}/api/${env.version}/${env.controller}/AddEmployee`,addEmployeeRequest);
  // }
  // DeleteEmployee(id: string): Observable<Employee> {
  //   return this.http.delete<Employee>(
  //     `${env.ServerRoot}/api/${env.version}/${env.controller}/DeleteEmployee/${id}`);
  // }


  GetStaticTrainees(): Promise<Trainee[]> {
    this.staticTrainees$ = [
      {
        "traineeId": 1,
        "firstName": "",
        "lastName": "",
        "email": null,
        "telephone": null,
        "photo": "http://bnetsource/uploads/photos/",
        "traineePfid": 12345,
        "reviewerPfid": 54321,
        "active": true
      },
      {
        "traineeId": 2,
        "firstName": "",
        "lastName": "",
        "email": null,
        "telephone": null,
        "photo": "http://bnetsource/uploads/photos/",
        "traineePfid": 23456,
        "reviewerPfid": 65432,
        "active": true
      },
      {
        "traineeId": 3,
        "firstName": "",
        "lastName": "",
        "email": null,
        "telephone": null,
        "photo": "http://bnetsource/uploads/photos/",
        "traineePfid": 34567,
        "reviewerPfid": 76543,
        "active": true
      },
      {
        "traineeId": 4,
        "firstName": "",
        "lastName": "",
        "email": null,
        "telephone": null,
        "photo": "http://bnetsource/uploads/photos/",
        "traineePfid": 45343,
        "reviewerPfid": 87654,
        "active": true
      },
    ];
    return Promise.resolve(this.staticTrainees$);
  }
}
