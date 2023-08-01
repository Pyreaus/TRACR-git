import { User } from 'src/app/Interfaces/User';
import { Scroller } from 'primeng/scroller';
import { Trainee } from 'src/app/Interfaces/Trainee';
import { UserService } from 'src/app/Services/UserService/user.service';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { AddModifyTraineeReq } from 'src/app/Interfaces/DTOs/AddModifyTraineeReq';
import { AddModifyDiaryReq } from 'src/app/Interfaces/DTOs/AddModifyDiaryReq';
import { HostListener, ViewChild, ViewEncapsulation, Renderer2, ChangeDetectorRef, ElementRef, AfterViewInit, AfterViewChecked, OnInit, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Diary } from 'src/app/Interfaces/Diary';
import { ActivatedRoute, Router } from '@angular/router';
import { DiaryTask } from 'src/app/Interfaces/DiaryTask';
import { NewDiaryTaskComponent } from './new-diarytask/new-diarytask';

enum status { Submitted='Submitted', SignedOff='SignedOff', UnderReview='UnderReview', Available='Available' }
enum UserType { Unauthorized='Unauthorized', Reviewer='Reviewer', Trainee='Trainee', Admin='Admin' }

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'], 
  encapsulation: ViewEncapsulation.Emulated
})
export class HOMEComponent implements OnInit, AfterViewInit, AfterViewChecked {
  @ViewChild(NewDiaryTaskComponent) newDiaryTaskComponent!: NewDiaryTaskComponent;
  @ViewChild('calendarElement', { static: false }) calendarElement!: ElementRef;
  @ViewChild('tdElements', { static: false }) tdElementsRef!: ElementRef[];
  @ViewChild('sc') sc!: Scroller;

  @HostListener('window:resize', ['$event']) onResize(event: Event) {  
    this.viewportMaximum = false;
  }

  step = 1;
  active!:string;
  overlayMax = '30px';
  statusColor = ['secondary','info'];
  user$!: User
  diaryReq!: AddModifyDiaryReq;
  viewportMaximum!:boolean;
  rowSelected!: string;
  barVisible!: boolean;
  currentDiary!: Diary;
  traineeToEdit!: Trainee;
  selectedReviewer$!: User;
  activeItem: any;
  userType$: BehaviorSubject<UserType> = new BehaviorSubject<UserType>(UserType.Unauthorized);
  diary: Diary = {DIARY_ID: 0,PFID: '',PRACTICE_AREA: '',WEEK_BEGINNING: '',LEARNING_POINTS: '',PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: '',PROFESSIONAL_CONDUCT_ISSUES: '',SIGN_OFF_SUBMITTED: 'false',SIGNED_OFF_BY: '',SHOW: ''}
  currentReviewer$!: string | null | undefined;
  currentReviewerPfid$!: undefined | string;
  currentTraineePfid$!: undefined | string;
  date!: Date[];
  weeks: string[] = []; 
  tasks$!: DiaryTask[];
  userDiaries!: Diary[];
  trainees$!: Trainee[];
  selectedTrainees!: User[];
  reviewers$: User[] = [];
  peopleFiltered$!: User[];
  currentTrainee$!: Trainee;
  items: string[][] = [];
  dateRange!:string;
  usersReviewer$!: User;
  formGroup!: FormGroup;
  diaryForm: FormGroup;
  signOffSubmitted!: boolean;
  diarySignedOff!: boolean;
  listenerAttached = false;
  calendarRendered = false;
  openWeekDisabled = true;
  rightBarVisible = false;
  modalOpen = false;
  disableSubmit = false;
  newDiaryPanel = false;
  ViewDiaryPanel = false;
  EditViewPanel = false;
  submitted = false;
  trainee = false;
  A: status = status.Available;
  B: status = status.Available;
  C: status = status.Available;
  D: status = status.Available;
  E: status = status.Available;

  constructor(
    private cdRef: ChangeDetectorRef,
    private elementRef: ElementRef,
    private renderer: Renderer2,
    private fb: FormBuilder,
    private router: Router,
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
        WEEK_BEGINNING: this!.reformatStartDate(this.dateRange),
        LEARNING_POINTS: this.diaryForm.value.LEARNING_POINTS,
        PRACTICE_AREA: this.diaryForm.value.PRACTICE_AREA,
        PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: this.diaryForm.value.PROFESSIONAL_DEVELOPMENT_UNDERTAKEN,
        PROFESSIONAL_CONDUCT_ISSUES: this.diaryForm.value.PROFESSIONAL_CONDUCT_ISSUES,
        SIGN_OFF_SUBMITTED: 'false',
        SIGNED_OFF_BY: '',
        SHOW: 'true'
      };
      this.userService.AddDiary(this.diaryReq).subscribe(res => console.trace(res));
      setTimeout(() => {this.reload()}, 1000);
  }
  ngAfterViewInit(): void {
    const resizeObserver = new ResizeObserver((entries) => {
      for (const entry of entries) {
      const isMaximized = (window.innerWidth >= window.screen.availWidth) && (window.outerHeight === window.screen.availHeight);
        if (isMaximized) {this.overlayMax='18px !important'} else {this.overlayMax='11px !important'}
        this.cdRef.detectChanges();
      }
    });
    resizeObserver.observe(window.document.documentElement);
  }
  ngOnDestroy(): void {
    this.userType$.unsubscribe();
  }
  formatTimestamp(timestamp: string): string {
    const [inputFormat, outputFormat]: [RegExp,RegExp] = [/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}/,
    /(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}).(\d{3})/];
    if (!inputFormat.test(timestamp)) return '';
    const [, year, month, day, hours, minutes, seconds, milliseconds] = timestamp.match(outputFormat) || [];
    if (!year || !month || !day || !hours || !minutes || !seconds || !milliseconds) return '';
    const formattedDateTime = `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
    return formattedDateTime;
  }
  ngOnInit(): void {
    this.userService.GetUsers().subscribe({
      next: (res:User[]) => {
        this.reviewers$ = res;
        this.reviewers$.forEach(rev => rev.FirstName = rev.FirstName+' '+rev.LastName);
        const codedPfids = [100]; 
        this.peopleFiltered$ = res.filter((trn) => !codedPfids.includes(trn.PFID));
      }, error: (err) => {console.trace(err)} });
    this.items = Array.from({ length: 1000 }).map((_, i) =>
      Array.from({ length: 1000 }).map((_j, j) => `Item #${i}_${j}`));
    setTimeout(() => {
      this.userService.getUserType().subscribe({
        next: (res:User) => {
          this.user$ = res;
          this.user$ ? this.userType$.next(this.user$.Role as UserType) : this.userType$.next(UserType.Unauthorized);
          this.userService.GetDiariesPfid(this.user$!.PFID).subscribe({
            next: (res:Diary[]) => {
              this.userDiaries = res;
            }, error: (err) => console.trace(err) 
          });
          this.userType$.value === UserType.Admin ? this.resetTrainees() : this.userType$.value === UserType.Reviewer ?
            this.userService.getTraineesByReviewer(this.user$.PFID).subscribe({
              next: (res:Trainee[]) => {
                this.trainees$ = res;
                this.trainees$.forEach(trn => trn.FirstName = trn.FirstName+' '+trn.LastName)
              }, error: (x) => console.trace(x)
            }) : void(0);
        }, error: (err) => {
          console.trace(err)
        }
      });
    }, 0);
  }
  resetTrainees(): void {
    this.userService.GetTrainees().subscribe({
      next: (res:Trainee[]) => {
        this.trainees$ = res;
        this.currentReviewerPfid$ = this.trainees$.find((trn: Trainee) => trn.TRAINEE_PFID === this.user$.PFID.toString())?.REVIEWER_PFID ?? undefined;
        this.trainees$.forEach(trn => trn.FirstName = trn.FirstName+' '+trn.LastName)}, error: (err) => {console.error(err)}
      });
  }
  updatePairs(reviewer: User,trainee: Trainee): void {
    const newReq: AddModifyTraineeReq = { REVIEWER_PFID: reviewer.PFID.toString(), ACTIVE: 'true', SHOW: 'true' };
    this.userService.SetPair(parseInt(trainee.TRAINEE_PFID!, 10), newReq).subscribe(res => console.trace(`api response: ${res}`));
    setTimeout(() => {this.reload()}, 100);
  }
  SetPairs(reviewer: User,users: User[]): void {
    const newReq: AddModifyTraineeReq = { REVIEWER_PFID: reviewer.PFID.toString(), ACTIVE: 'true', SHOW: 'true' };
    for (let T of users) this.userService.AssignTrainees(T.PFID, newReq).subscribe(res => console.trace(res));
    setTimeout(() => {this.reload()}, 100);
  }
  getDiaryTasks(diaryId: number): void {
    this.userService.GetTasksDiaryId(diaryId).subscribe({
      next: (res:DiaryTask[]) => {this.tasks$ = res}, error: (err) => {console.error(err)}
    });
  }
  getReviewerByPfid(pfid: string): User | undefined {
    return this.reviewers$.find((rev: User) => rev.PFID === parseInt(pfid, 10));
  }
  ngAfterViewChecked(): void {
    setTimeout(() => {
      if (!this.calendarRendered) {
        const calendarElement = this.elementRef.nativeElement.querySelector('.p-datepicker-header');
        if (calendarElement) {
          let [next, prev] = [this.elementRef.nativeElement.querySelectorAll('.p-datepicker-next')[0],
          this.elementRef.nativeElement.querySelectorAll('.p-datepicker-prev')[0]];
          this.calendarRendered = true;
          this.markCells();
          if (next && prev) {
            this.renderer.listen(next,'click', (event: Event) => {
              this.markCells();
              [this.statusColor,  this.rowSelected] = [['secondary','info'], 'cal'];
              this.calendarRendered = false;
            });
            this.renderer.listen(prev,'click', (event: Event) => {
              this.markCells();
              [this.statusColor,  this.rowSelected] = [['secondary','info'], 'cal'];
              this.calendarRendered = false;
            });
          }
        }
      }
    }, 1000)
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
  selected(num: number, special: boolean = false, ignore: boolean = false): void {
    let monthNumber: number[] = [1,2,3,4,5,6,7,8,9,10,11,12];
    this.statusColor = ignore ? this.statusColor : ['secondary','info'];
    this.openWeekDisabled = ignore ? true : false;
    this.step = ignore ? this.step : 2;
    let month!: number;
    let weekRange!: string[];
    let year: string =  this.elementRef.nativeElement.querySelectorAll(
    '.p-datepicker-year')[0].innerHTML.replace(/\s/g,'').slice(-2);
    let monthName: string[] = ['January','February',
    'March','April','May','June','July','August',
    'September','October','November','December'];
    for(let i = 0; i < monthName.length; i++){
      if(this.elementRef.nativeElement.querySelectorAll('.p-datepicker-month')[0].innerHTML.replace(/\s/g,'') === monthName[i]){
        month = monthNumber[i]}
    }
    switch (num) {
      case 1:
        this.active = 'A';
        let [day1w1, day2w1]: [number, number] = [this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[special ? 1 : 0].innerHTML.replace(/\D/g,''),
        this.elementRef.nativeElement.querySelectorAll('.p-ripple.p-disabled')[6].innerHTML.replace(/\D/g,'')];
        let startMonth: number = Number(day1w1) < Number(day2w1) ? month : month - 1;
        [weekRange, this.rowSelected] = [[`${day1w1}/${startMonth}/${year}`,
        `${day2w1}/${month}/${year}`],ignore ? this.rowSelected : 'cal1'];
        break;
      case 2:
        this.active = 'B';
        [weekRange, this.rowSelected] = [[`${this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[special ? 8 : 7].innerHTML.replace(/\D/g,'')}/${month}/${year}`,
        `${this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[13].innerHTML.replace(/\D/g,'')}/${month}/${year}`], ignore ? this.rowSelected : 'cal2'];
        break
      case 3:
        this.active = 'C';
        [weekRange, this.rowSelected] = [[`${this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[special ? 15 : 14].innerHTML.replace(/\D/g,'')}/${month}/${year}`,
        `${this.elementRef.nativeElement.querySelectorAll('.p-ripple.p-disabled')[20].innerHTML.replace(
          /\D/g,'')}/${month}/${year}`], ignore ? this.rowSelected : 'cal3'];
        break;
      case 4:
        this.active = 'D';
        [weekRange, this.rowSelected] = [[`${this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[special ? 22 : 21].innerHTML.replace(/\D/g,'')}/${month}/${year}`,
          `${this.elementRef.nativeElement.querySelectorAll('.p-ripple.p-disabled')[27].innerHTML.replace(
            /\D/g,'')}/${month}/${year}`],ignore ? this.rowSelected : 'cal4'];
        break;
      case 5:
        this.active = 'E';
        let [day1w4, day2w4]: [number, number] = [this.elementRef.nativeElement.querySelectorAll(
          '.p-ripple.p-disabled')[special ? 29 : 28].innerHTML.replace(/\D/g,''),
          this.elementRef.nativeElement.querySelectorAll('.p-ripple.p-disabled')[34].innerHTML.replace(/\D/g,'')];
        let endMonth: number = Number(day1w4) < Number(day2w4) ? month : month + 1;
        [weekRange, this.rowSelected] = [[`${day1w4}/${month}/${year}`, `${day2w4}/${endMonth}/${year}`], ignore ? this.rowSelected : 'cal5'];
        break;
    }
    this.dateRange = this.reformatStartDate(`${weekRange[0]} - ${weekRange[1]}`);
    console.log(this.active);
  }
  openNewDiary(): void {
    this.step = 3;
    setTimeout(() => {
      this.ViewDiaryPanel = false;
      this.newDiaryPanel = true;
    },10);
  }
  markCells(): void {
    let pointer = 0;
    this.weeks = [];
    for (let i = 0; i < 5; i++) {
      this.selected(i+1,true,true);
      this.weeks.push(this.dateRange);
    }
    [this.A,this.B,this.C,this.D,this.E,] = [status.Available,
    status.Available,status.Available,status.Available,status.Available];
    for (let week of this.weeks) {
      pointer++
      for (let diary of this.userDiaries) {
        if (diary.WEEK_BEGINNING!.slice(0,-9).toString() == week) {
          switch (pointer) {
            case 1:
              if (diary.SIGNED_OFF_TIMESTAMP != null) {
                this.A = status.SignedOff;
              } else if ((diary.SIGN_OFF_SUBMITTED == 'true') || (diary.SIGN_OFF_SUBMITTED == 'True')) {
                this.A = status.Submitted;
              } else this.A = status.UnderReview;
            break;
            case 2:
              if (diary.SIGNED_OFF_TIMESTAMP != null) {
                this.B = status.SignedOff;
              } else if ((diary.SIGN_OFF_SUBMITTED == 'true') || (diary.SIGN_OFF_SUBMITTED == 'True')) {
                this.B = status.Submitted;
              } else this.B = status.UnderReview;
            break;
            case 3:
              if (diary.SIGNED_OFF_TIMESTAMP != null) {
                this.C = status.SignedOff;
              } else if ((diary.SIGN_OFF_SUBMITTED == 'true') || (diary.SIGN_OFF_SUBMITTED == 'True')) {
                this.C = status.Submitted;
              } else this.C = status.UnderReview;
            break;
            case 4:
              if (diary.SIGNED_OFF_TIMESTAMP != null) {
                this.D = status.SignedOff;
              } else if ((diary.SIGN_OFF_SUBMITTED == 'true') || (diary.SIGN_OFF_SUBMITTED == 'True')) {
                this.D = status.Submitted;
              } else this.D = status.UnderReview;
            break;
            case 5:
              if (diary.SIGNED_OFF_TIMESTAMP != null) {
                this.E = status.SignedOff;
              } else if ((diary.SIGN_OFF_SUBMITTED == 'true') || (diary.SIGN_OFF_SUBMITTED == 'True')) {
                this.E = status.Submitted;
              } else this.E = status.UnderReview;
            break;
          }
        }
      }
    }
    pointer = 0;
    this.cdRef.detectChanges();
    console.log('finished: ' + this.A + this.B + this.C + this.D + this.E);
  }
  openViewDiary(trn:boolean = false): void {
    let match: boolean = false;
    if (trn) for (let diary of this.userDiaries) {
        if (this.dateRange == diary!.WEEK_BEGINNING!.slice(0,-9)!.toString()!) {
          console.log(diary.WEEK_BEGINNING!.slice(0,-9)!.toString()! + ' ' + this.dateRange);
          this.currentDiary = diary;
          this.diarySignedOff = diary.SIGNED_OFF_TIMESTAMP != null ? true : false;
          this.signOffSubmitted = (diary.SIGN_OFF_SUBMITTED=='True')||(diary.SIGN_OFF_SUBMITTED=='true') ? true : false;
          this.getDiaryTasks(this.currentDiary.DIARY_ID!)
          this.newDiaryPanel = false;
          this.ViewDiaryPanel = true;
          [match, this.step] = [true, 4];
        }
      if (match) break;
    }
    if (trn && match == false) return this.openNewDiary();
    trn == false ? this.userService.GetTrainees().subscribe({
      next: (res:Trainee[]) => {
        this.currentTrainee$ = this.trainees$.find((trn: Trainee) => trn.TRAINEE_PFID==this.user$.PFID.toString())!
        this.usersReviewer$ = this.reviewers$.find((rev: User) => rev.PFID.toString()==this.currentTrainee$.REVIEWER_PFID)!
      }, error: (err:any) => console.trace(err)
    }) : void(0);
    if (trn == false) {
      for (let diary of this.userDiaries) {
        if (this.dateRange == diary!.WEEK_BEGINNING!.slice(0,-9)!.toString()!) {
          this.currentDiary = diary;
          this.diarySignedOff = diary.SIGNED_OFF_TIMESTAMP != null ? true : false;
          this.signOffSubmitted = (diary.SIGN_OFF_SUBMITTED=='True')||(diary.SIGN_OFF_SUBMITTED=='true') ? true : false;
          this.getDiaryTasks(this.currentDiary.DIARY_ID!)
          this.newDiaryPanel = false;
          this.ViewDiaryPanel = true;
          [match, this.step] = [true, 3]
        }
        if (match) break;
      }
      if (!match) this.statusColor = ["secondary","danger"];
      this.trainees$ = this.trainees$.filter((T: Trainee) => T.TRAINEE_PFID == this.currentTraineePfid$!.toString());
    }
  }
  SubmitSignOff(): void {
    this.disableSubmit = true;
    const diaryReq: AddModifyDiaryReq = {
      PFID: this.user$!.PFID.toString(),
      WEEK_BEGINNING: this.currentDiary.WEEK_BEGINNING!.slice(0,-9)!.toString()!,
      LEARNING_POINTS: this.currentDiary.LEARNING_POINTS!,
      PRACTICE_AREA: this.currentDiary.PRACTICE_AREA!,
      PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: this.currentDiary.PROFESSIONAL_DEVELOPMENT_UNDERTAKEN!,
      PROFESSIONAL_CONDUCT_ISSUES: this.currentDiary.PROFESSIONAL_CONDUCT_ISSUES,
      SIGN_OFF_SUBMITTED: 'True',
      SIGNED_OFF_BY: null
    }
    console.log(diaryReq);
    this.userService.EditDiaryById(this.currentDiary.DIARY_ID!,diaryReq).subscribe({
      next: (res) => console.log(res),
      error: (err) => console.error(err)
    });
  }
  SignOff(trnDiary: Diary): void {
    if (this.diarySignedOff || !this.signOffSubmitted) return void(0);
    this.diarySignedOff = true;
    const diaryReq: AddModifyDiaryReq = {
      PFID: trnDiary.PFID!,
      WEEK_BEGINNING: trnDiary.WEEK_BEGINNING!.slice(0,-9)!.toString()!,
      LEARNING_POINTS: trnDiary.LEARNING_POINTS!,
      PRACTICE_AREA: trnDiary.PRACTICE_AREA!,
      PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: trnDiary.PROFESSIONAL_DEVELOPMENT_UNDERTAKEN!,
      PROFESSIONAL_CONDUCT_ISSUES: trnDiary.PROFESSIONAL_CONDUCT_ISSUES,
      SIGN_OFF_SUBMITTED: 'True',
      SIGNED_OFF_BY: this.user$!.PFID.toString()
    }
    this.userService.EditDiaryById(trnDiary.DIARY_ID!,diaryReq).subscribe({
      next: (_) => {
        this.userService.GetDiariesPfid(parseInt(this.currentTraineePfid$!,10)).subscribe({
          next: (res: Diary[]) => {
            this.userDiaries = res;
            this.currentDiary = this.currentDiary.DIARY_ID ? this.userDiaries.find((d: Diary) =>
             d.DIARY_ID === this.currentDiary.DIARY_ID)! : this.userDiaries[0];
            this.markCells();
          }, error: (err) => console.trace(err)
        });
        setTimeout(() => this.markCells(), 2000)
      }, error: (err) => console.trace(err)
    });
  }
  openEditView(tPfid: string): void {
    this.trainees$ = this.trainees$.filter((T: Trainee) => T.TRAINEE_PFID == tPfid.toString());
    this.traineeToEdit = this.trainees$.filter((T: Trainee) => T.TRAINEE_PFID === tPfid.toString())[0];
    this.EditViewPanel = true
  }
  openTraineeDiary(tPfid: string): void {
    this.trainees$ = this.trainees$.filter((T: Trainee) => T.TRAINEE_PFID === tPfid);
    this.userService.GetDiariesPfid(parseInt(tPfid, 10)).subscribe({
      next: (res:Diary[]) => {
        [this.userDiaries, this.currentTraineePfid$] = [res, tPfid];
      }, error: (err) => console.error(err)
    }); 
    this.ViewDiaryPanel = true;
    this.step = 2;
  }
  reformatDate(date: string): string {
    const dateParts = date.split("/");
    if (dateParts.length !== 3) return '';
    let [day, month, year] = dateParts;
    const formattedDate = `20${year}-${this.padZero(Number(month))}-${this.padZero(Number(day))}`;
    return formattedDate;
  }
  
  reformatStartDate(dateRange: string): string {
    const dateParts = dateRange.split(" - ");
    const startDate = this.reformatDate(dateParts[0]);
    return startDate;
  }
  padZero(value: number): string {
    return value.toString().padStart(2, "0");
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