import { User } from 'src/app/Interfaces/User';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Trainee } from 'src/app/Interfaces/Trainee';
import { env } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { AddModifyTraineeReq } from 'src/app/Interfaces/DTOs/AddModifyTraineeReq';
import { AddModifyDiaryReq } from 'src/app/Interfaces/DTOs/AddModifyDiaryReq';
import { Diary } from 'src/app/Interfaces/Diary';
import { DiaryTask } from 'src/app/Interfaces/DiaryTask';
import { AddModifyTaskReq } from 'src/app/Interfaces/DTOs/AddModifyTaskReq';
import { Skill } from 'src/app/Interfaces/Skill';

@Injectable({
  providedIn: 'root'
}) export class UserService {

  staticTrainees$!: Trainee[];
  constructor(private http: HttpClient) {}

  SetPair(PfId: number, addReq: AddModifyTraineeReq): Observable<AddModifyTraineeReq> {
    return this.http.put<AddModifyTraineeReq>(`${env.ApiUrl}/${env.version}/${env.usrController}/SetPair/${PfId}`, addReq);
  }
  GetTasksDiaryId(DiaryId: number): Observable<DiaryTask[]> {
    return this.http.get<DiaryTask[]>(`${env.ApiUrl}/${env.version}/${env.diaryController}/GetTasksByDiaryId/${DiaryId}`)
    .pipe(map((res:DiaryTask[]) => {
      const diaryTasks: DiaryTask[] = res.map((obj:any) => this.deserialize('Task', obj, true));
      return diaryTasks;
    }));
  }
  GetUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetUsers`)
    .pipe(map((res:User[]) => {
      const users: User[] = res.map((obj:any) => this.deserialize('Usr|Rev', obj, true))
      return users;
    }));
  }
  GetDiariesPfid(PfId: number): Observable<Diary[]> {
    return this.http.get<Diary[]>(`${env.ApiUrl}/${env.version}/${env.diaryController}/GetDiariesPfid/${PfId}`)
     .pipe(map((res:Diary[]) => {
      const diaries: Diary[] = res.map((obj:any) => this.deserialize('Diary', obj, true))
      return diaries;
    }));
  }
  getTraineesByReviewer(PfId: number): Observable<Trainee[]> {
    return this.http.get<Trainee[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetTraineesByReviewer/${PfId}`)
    .pipe(map((res:Trainee[]) => {
      const trainees: Trainee[] = res.map((obj:any) => this.deserialize('Trainee', obj, true))
      return trainees;
    }));
  }
  EditDiaryById(DiaryId: number, addDiaryRequest: AddModifyDiaryReq): Observable<AddModifyDiaryReq> {
    return this.http.put<AddModifyDiaryReq>(`${env.ApiUrl}/${env.version}/${env.diaryController}/EditDiaryById/${DiaryId}`, addDiaryRequest);
  }
  AssignTrainees(PfId: number, addReq: AddModifyTraineeReq): Observable<AddModifyTraineeReq> {
    return this.http.post<AddModifyTraineeReq>(`${env.ApiUrl}/${env.version}/${env.usrController}/AssignTrainees/${PfId}`, addReq);
  }
  GetReviewers(): Observable<User[]> {
    return this.http.get<User[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetReviewers`)
    .pipe(map((res:User[]) => {
      const reviewers: User[] = res.map((obj:any) => this.deserialize('Usr|Rev', obj, true))
      return reviewers
    }));
  }
  GetTrainees(): Observable<Trainee[]> {
    return this.http.get<Trainee[]>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetTrainees`)
    .pipe(map((res:Trainee[]) => {
      const trainees: Trainee[] = res.map((obj:any) => this.deserialize('Trainee', obj, true))
      return trainees;
    }));
  }
  GetUserReviewer(PfId: number): Observable<User> {
    return this.http.get<User>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetUserReviewer/${PfId}`)
    .pipe(map(res => this.deserialize('Usr|Rev', res, false)));
  }
  GetTrainee(PfId: number): Observable<Trainee> {
    return this.http.get<Trainee>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetTrainee/${PfId}`)
    .pipe(map(res => this.deserialize('Trainee', res, false)));
  }
  getUserType(): Observable<User> {
    return this.http.get<User>(`${env.ApiUrl}/${env.version}/${env.usrController}/GetUserType`)
    .pipe(map(res => this.deserialize('Usr|Rev', res, false)));
  }
  GetSkills(): Observable<Skill[]> {
    return this.http.get<Skill[]>(`${env.ApiUrl}/${env.version}/${env.diaryController}/GetSkills`);
  }
  ModifyDiaryTask(DiaryId: number, addModifyTaskRequest: AddModifyTaskReq): Observable<AddModifyTaskReq> {
    return this.http.put<AddModifyTaskReq>(`${env.ApiUrl}/${env.version}/${env.diaryController}/EditTaskByTaskId/${DiaryId}`,addModifyTaskRequest);
  }
  AddDiary(addDiaryRequest: AddModifyDiaryReq): Observable<AddModifyDiaryReq> {
    return this.http.post<AddModifyDiaryReq>(`${env.ServerRoot}/api/${env.version}/${env.diaryController}/AddDiary`,addDiaryRequest);
  }
  AddDiaryTask(addDiaryTaskRequest: AddModifyTaskReq): Observable<AddModifyTaskReq> {
    return this.http.post<AddModifyTaskReq>(`${env.ApiUrl}/${env.version}/${env.diaryController}/AddDiaryTask`,addDiaryTaskRequest);
  }
  DeleteTask(TaskId: number): Observable<DiaryTask> {
    return this.http.delete<DiaryTask>(`${env.ApiUrl}/${env.version}/${env.diaryController}/DeleteTaskByTaskId/${TaskId}`);
  }
  deserialize(entity:string, json:any, array:boolean): any {
    switch (entity) {
      case 'Task':
        return {
          DIARY_TASK_ID: json.diarY_TASK_ID,
          DIARY_ID: json.diarY_ID,
          MATTER: json.matter,
          FEE_EARNERS: json.feE_EARNERS,
          TASK_DESCRIPTION: json.tasK_DESCRIPTION,
          SKILLS: json.skills,
          SHOW: json.show,
        };
        case 'Diary':
          return {
            PFID: json.pfid,
            DIARY_ID: json.diarY_ID,
            PRACTICE_AREA: json.practicE_AREA,
            WEEK_BEGINNING: json.weeK_BEGINNING,
            LEARNING_POINTS: json.learninG_POINTS,
            PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: json.professionaL_DEVELOPMENT_UNDERTAKEN,
            PROFESSIONAL_CONDUCT_ISSUES: json.professionaL_CONDUCT_ISSUES,
            SIGN_OFF_SUBMITTED: json.sigN_OFF_SUBMITTED,
            SIGNED_OFF_TIMESTAMP: json.signeD_OFF_TIMESTAMP,
            SIGNED_OFF_BY: json.signeD_OFF_BY,
            SHOW: json.show 
          };
      case 'Trainee':
        return {
            FirstName: json.firstName,
            LastName: json.lastName,
            Photo: json.photo,
            Email: json.email,
            Telephone: json.telephone,
            TRAINEE_PFID: json.traineE_PFID,
            REVIEWER_PFID: json.revieweR_PFID,
            OTHER_PFID: json.otheR_PFID,
            ACTIVE: json.active,
            SHOW: json.show
        };
      case 'Usr|Rev':
        return {
          PFID: json.pfid,
          FirstName: json.firstName,
          LastName: json.lastName,
          Photo: json.photo,
          Role: json.role
        };  
    };
  }

  GetStaticTrainees(): Promise<Trainee[]> {
    // this.staticTrainees$ = []
    return Promise.resolve(this.staticTrainees$);
  }
}
