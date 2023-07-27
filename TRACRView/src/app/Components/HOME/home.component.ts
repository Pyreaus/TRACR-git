import { User } from 'src/app/Interfaces/User';
import { Scroller } from 'primeng/scroller';
import { Trainee } from 'src/app/Interfaces/Trainee';
import { UserService } from 'src/app/Services/UserService/user.service';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { AddModifyTraineeReq } from 'src/app/Interfaces/DTOs/AddModifyTraineeReq';
import { AddModifyDiaryReq } from 'src/app/Interfaces/DTOs/AddModifyDiaryReq';
import { Component, OnInit, ViewChild, ViewEncapsulation, Renderer2, ElementRef, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Diary } from 'src/app/Interfaces/Diary';
import { ActivatedRoute, Router } from '@angular/router';
import { DiaryTask } from 'src/app/Interfaces/DiaryTask';
import { NewDiaryTaskComponent } from './new-diarytask/new-diarytask';

enum UserType {
  Unauthorized='Unauthorized',
  Reviewer='Reviewer',
  Trainee='Trainee',
  Admin='Admin' }

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'], encapsulation: ViewEncapsulation.Emulated
})
export class HOMEComponent implements OnInit, AfterViewInit {
  @ViewChild('sc') sc!: Scroller;
  @ViewChild('tdElements', { static: false }) tdElementsRef!: ElementRef[];
  @ViewChild(NewDiaryTaskComponent) newDiaryTaskComponent!: NewDiaryTaskComponent;

  step = 1;
  diaryReq!: AddModifyDiaryReq;
  rowSelected!: string;
  barVisible!: boolean;
  activeItem: any;
  diary: Diary = {DIARY_ID: 0, PFID: '', PRACTICE_AREA: '', WEEK_BEGINNING: '',LEARNING_POINTS: '',PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: '',PROFESSIONAL_CONDUCT_ISSUES: '',SIGN_OFF_SUBMITTED: 'false', SIGNED_OFF_BY: '',SHOW: ''}
  currentDiary!: Diary;
  selectedReviewer$!: User;
  traineeToEdit!: Trainee;
  userType$: BehaviorSubject<UserType> = new BehaviorSubject<UserType>(UserType.Unauthorized);
  currentReviewer$!: string | null | undefined;
  currentReviewerPfid$!: undefined | string;
  date!: Date[];
  tasks$!: DiaryTask[];
  trainees$!: Trainee[];
  selectedTrainees!: User[];
  dateRange!:string;
  reviewers$: User[] = [];
  peopleFiltered$!: User[];
  currentTrainee$!: Trainee;
  items: string[][] = [];
  user$!: User
  formGroup!: FormGroup;
  diaryForm: FormGroup;
  rightBarVisible = false;
  modalOpen = false;
  disableSubmit = false;
  newDiaryPanel = false;
  ViewDiaryPanel = false;
  EditViewPanel = false;
  submitted = false;
  trainee = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private elementRef: ElementRef,
    private userService: UserService
    ) {
    [this.rowSelected,this.barVisible] = ['cal',true];
    this.diaryForm = this.fb.group({
      DIARY_ID: [{ value: '', disabled: true }, Validators.required],
      WEEK_BEGINNING: [null, Validators.required],
      LEARNING_POINTS: [null, Validators.required],
      PRACTICE_AREA: [null, Validators.required],
      PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: [null, Validators.required],
      PROFESSIONAL_CONDUCT_ISSUES: [null, Validators.required],
      SIGN_OFF_SUBMITTED: [false, Validators.required],
      SIGNED_OFF_BY: [null],
      SHOW: [true, Validators.required]
    });
  }
  onSubmitNewDiary(): void {
      this.diaryReq = {
        PFID: this.user$.PFID.toString(),
        WEEK_BEGINNING: this!.dateRange,
        LEARNING_POINTS: this.diaryForm.value.LEARNING_POINTS,
        PRACTICE_AREA: this.diaryForm.value.PRACTICE_AREA,
        PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: this.diaryForm.value.PROFESSIONAL_DEVELOPMENT_UNDERTAKEN,
        PROFESSIONAL_CONDUCT_ISSUES: this.diaryForm.value.PROFESSIONAL_CONDUCT_ISSUES,
        SIGN_OFF_SUBMITTED: 'false',
        SIGNED_OFF_BY: '',
        SHOW: 'true'
      };
      console.log(this.diaryReq);
      // this.userService.AddDiary(this.diaryReq).subscribe(res => console.log(res));
      setTimeout(() => {
        this.openViewDiary()
      }, 1000);
  }
  ngOnDestroy(): void {
    this.userType$.unsubscribe();
  }
  ngOnInit(): void {
    this.userService.GetUsers().subscribe({
      next: (res) => {
        this.reviewers$ = res;
        console.info(res[0].FirstName);
        this.reviewers$.forEach(rev => rev.FirstName = rev.FirstName+' '+rev.LastName);
        const codedPfids = [100]; //Maintenance ---------------
        this.peopleFiltered$ = res.filter((trn) => !codedPfids.includes(trn.PFID));
      }, error: (err) => {console.error(err)} });
    this.items = Array.from({ length: 1000 }).map((_, i) =>
      Array.from({ length: 1000 }).map((_j, j) => `Item #${i}_${j}`));
    setTimeout(() => {
      this.userService.getUserType().subscribe({
        next: (res) => {
          this.user$ = res;
          console.info(res);
          // this.userService.GetTrainee(this.user$.PFID).subscribe({
          //   next: (res) => {
          //     this.currentTrainee$ = res;
          //   }, error: (err) => {console.error(err)} });
          this.user$ ? this.userType$.next(this.user$.Role as UserType) : this.userType$.next(UserType.Unauthorized);
          console.info(this.userType$.value) //dev only - remove
          this.userType$.value === UserType.Admin ? this.resetTrainees() : this.userType$.value === UserType.Reviewer ?
            this.userService.getTraineesByReviewer(this.user$.PFID).subscribe({
              next: (res) => {
                this.trainees$ = res;
                this.trainees$.forEach(trn => trn.FirstName = trn.FirstName+' '+trn.LastName)}, error: (x) => {console.error(x)}
            }) : void(0);
            // this.userType$.next(UserType.Admin); --  dev
        }, error: (err) => {console.error(err)}
      });
    }, 200);
  }
  resetTrainees(): void {
    this.userService.GetTrainees().subscribe({
      next: (res) => {
        this.trainees$ = res;
        this.currentReviewerPfid$ = this.trainees$.find((trn: Trainee) => trn.TRAINEE_PFID === this.user$.PFID.toString())?.REVIEWER_PFID ?? undefined;
        this.trainees$.forEach(trn => trn.FirstName = trn.FirstName+' '+trn.LastName)}, error: (err) => {console.error(err)}
      });
  }
  getReviewerByPfid(pfid: string): User | undefined {
    return this.reviewers$.find((rev: User) => rev.PFID === parseInt(pfid, 10));
  }
  getDiaryTasks(diaryId: number): void {
    this.userService.GetTasksDiaryId(diaryId).subscribe({
      next: (res) => {this.tasks$ = res}, error: (err) => {console.error(err)}
    });
  }
  updatePairs(reviewer: User,trainee: Trainee): void {
    const newReq: AddModifyTraineeReq = { REVIEWER_PFID: reviewer.PFID.toString(), ACTIVE: 'true', SHOW: 'true' };
    this.userService.SetPair(parseInt(trainee.TRAINEE_PFID!, 10), newReq).subscribe(res => console.info("http response: " + res));
    window.location.reload();
  }
  SetPairs(reviewer: User,users: User[]): void {
    const newReq: AddModifyTraineeReq = { REVIEWER_PFID: reviewer.PFID.toString(), ACTIVE: 'true', SHOW: 'true' };
    for (let T of users) this.userService.AssignTrainees(T.PFID, newReq).subscribe(res => console.log(res));
    window.location.reload();
  }
  menuVal = 'Current';
  stateOptions2: any[] = [
    { label: 'Current', menuVal: 'Current' },
    { label: 'Past', menuVal: 'Past', constant: true },
  ];
  stateOptions: any[] = [
    { label: 'Current', menuVal: 'Current' },
    { label: 'Past', menuVal: 'Past', constant: true },
    { label: 'New', menuVal: 'New'}
  ];
  selected(num: number): void {
    let month!: number;
    this.step = 2;
    // if (!this.disableSubmit) this.step = 2;
    // console.log(this.currentDiary.diaryId)
    let monthNumber: number[] = [1,2,3,4,5,6,7,8,9,10,11,12];
    let weekRange!: string[];
    let year: string =  this.elementRef.nativeElement.querySelectorAll('.p-datepicker-year')
      [0].innerHTML.replace(/\s/g,'').slice(-2);
    let monthName: string[] = ['January','February','March','April','May','June','July','August','September','October','November','December'];
    for(let i = 0; i < monthName.length; i++){
      if(this.elementRef.nativeElement.querySelectorAll('.p-datepicker-month')[0].innerHTML.replace(/\s/g,'') === monthName[i]){
        month = monthNumber[i]}
    }
    switch(num){
      case 1:
        let [day1w1,day2w1]: [number,number] = [this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[0].innerHTML.replace(/\D/g,''),
          this.elementRef.nativeElement.querySelectorAll('.p-ripple.p-disabled')[6].innerHTML.replace(/\D/g,'')
        ];
        let startMonth:number = Number(day1w1) < Number(day2w1) ? month : month - 1;
        [weekRange,this.rowSelected] = [[`${day1w1}/${startMonth}/${year}`,`${day2w1}/${month}/${year}`],'cal1'];
          break;
        case 2:
          [weekRange,this.rowSelected] = [[`${this.elementRef.nativeElement.querySelectorAll(
            '.p-ripple.p-disabled')[7].innerHTML.replace(/\D/g,'')}/${month}/${year}`,
        `${this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[13].innerHTML.replace(/\D/g,'')}/${month}/${year}`],'cal2'];
          break
        case 3:
          [weekRange,this.rowSelected] = [[`${this.elementRef.nativeElement.querySelectorAll(
            '.p-ripple.p-disabled')[14].innerHTML.replace(/\D/g,'')}/${month}/${year}`,
        `${this.elementRef.nativeElement.querySelectorAll('.p-ripple.p-disabled')[20].innerHTML.replace(/\D/g,'')}/${month}/${year}`],'cal3'];
          break;
        case 4:
          [weekRange,this.rowSelected] = [[`${this.elementRef.nativeElement.querySelectorAll(
            '.p-ripple.p-disabled')[21].innerHTML.replace(/\D/g,'')}/${month}/${year}`,`${this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[27].innerHTML.replace(/\D/g,'')}/${month}/${year}`],'cal4'];
          break;
        case 5:
          let [day1w4,day2w4]: [number,number] = [this.elementRef.nativeElement.querySelectorAll(
            '.p-ripple.p-disabled')[28].innerHTML.replace(/\D/g,''),this.elementRef.nativeElement.querySelectorAll(
            '.p-ripple.p-disabled')[34].innerHTML.replace(/\D/g,'')];
          let endMonth:number = Number(day1w4) < Number(day2w4) ? month : month + 1;
          [weekRange,this.rowSelected] = [[`${day1w4}/${month}/${year}`,`${day2w4}/${endMonth}/${year}`],'cal5'];
          break;
      }
      this.dateRange = `${weekRange[0]} - ${weekRange[1]}`;
  }
  openNewDiary(): void {
    this.step = 3;
    setTimeout(() => {
      this.newDiaryPanel = true;
      this.ViewDiaryPanel = false;   //change to false after dev
    },10);
  }
  ngAfterViewInit(): void {
    setTimeout(() => {
      // console.error(this.menuVal)
    },5000);
  }
  openViewDiary(): void {
    // this.getDiaryTasks();
    this.step = 4;
    this.newDiaryPanel = false;
    this.ViewDiaryPanel = true;   //change to false after dev
    this.currentReviewer$ = 'Peter Johnson'  //fix bug, make new call using this.user$!.otherPfid
    this.userService.GetDiaryPfid(this.user$!.PFID).subscribe({
      next: (res) => {
        this.currentDiary = res;
        this.getDiaryTasks(this.currentDiary.DIARY_ID!);
      },
      error: (err) => {
        console.error(err)
      }
    });
    // this.userService.GetDiaryPfid(this.user$!.otherPfid).subscribe({next: (res) => { this.currentDiary = res; },
    // error: (err) => {console.error(err)} });
  }
  SubmitSignOff(): void {
    this.disableSubmit = true;
    const diaryReq: AddModifyDiaryReq = {
      PFID: this.user$!.PFID.toString(),
      WEEK_BEGINNING: this.currentDiary.WEEK_BEGINNING!,
      LEARNING_POINTS: this.currentDiary.LEARNING_POINTS!,
      PRACTICE_AREA: this.currentDiary.PRACTICE_AREA!,
      PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: this.currentDiary.PROFESSIONAL_DEVELOPMENT_UNDERTAKEN!,
      PROFESSIONAL_CONDUCT_ISSUES: this.currentDiary.PROFESSIONAL_CONDUCT_ISSUES,
      SIGN_OFF_SUBMITTED: 'true',
      SIGNED_OFF_BY: 'VALUE HARDOCED FOR DEVELOPMENT - UPDATE'
    }
    console.log(diaryReq);
    this.userService.EditDiaryPfid(this.user$!.PFID,diaryReq).subscribe({
      next: (res) => console.log(res),
      error: (err) => console.error(err)
    });
  }
  // openUserDiary(traineePfid: string): void {
  //   console.info(`Opening diary for ${traineePfid}`);
  // }
  openEditView(tPfid: string): void {
    this.trainees$ = this.trainees$.filter((T: Trainee) => T.TRAINEE_PFID == tPfid.toString());
    // console.log(this.trainees$[0] + 'rev: ' + this.getReviewerByPfid(this.trainees$[0].reviewerPfid!)?.firstName);
    this.traineeToEdit = this.trainees$.filter((T: Trainee) => T.TRAINEE_PFID === tPfid.toString())[0];
    this.EditViewPanel = true
  }
  openTraineeDiary(tPfid: string): void {
    this.userService.GetDiaryPfid(parseInt(tPfid, 10)).subscribe({
      next: (res) => {
        this.currentDiary = res;
        this.getDiaryTasks(this.currentDiary.DIARY_ID!);
      }, error: (err) => console.error(err)
    });
    this.ViewDiaryPanel = true;
    this.step = 2;
  }
  openTaskModal(): void {
    this.modalOpen = true;
  }
  reload(): void {
    window.location.reload();
  }
  reset(): void {
    this.sc.scrollToIndex(0,'smooth')
  }
  taskModalSubmitted(): void {
    this.newDiaryTaskComponent.createNewTask();
    setTimeout(() => {
      this.getDiaryTasks(this.currentDiary.DIARY_ID!);
    }, 1000);
  }
}