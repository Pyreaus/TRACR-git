import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Diary } from 'src/app/Interfaces/Diary';
import { DiaryTask } from 'src/app/Interfaces/DiaryTask';
import { UserService } from 'src/app/Services/UserService/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddModifyTaskReq } from 'src/app/Interfaces/DTOs/AddModifyTaskReq';
import { User } from 'src/app/Interfaces/User';
import { Skill } from 'src/app/Interfaces/Skill';
// import { SkillDTO } from 'src/app/Interfaces/DTOs/SkillDTO';

@Component({
  selector: 'new-diarytask',
  templateUrl: './new-diarytask.html',
  styles: [`
  :host ::ng-deep {
    .p-multiselect .p-multiselect-label, .p-checkbox-box { z-index:1000000 !important; background-color: white !important; }
    .p-multiselect { border: 1px solid #cdcdcd !important; min-width:458px !important; max-width:458px !important }
    .p-multiselect-filter { background-color: #838a97 !important; }
    .p-multiselect-trigger, .p-multiselect-header, .p-multiselect-items { background-color: #6c757d !important; }
    .p-multiselect-open	{ z-index:100000000 !important; }
  }`]
})
export class NewDiaryTaskComponent implements OnInit {
  @Input() diary!: Diary;
  @Output() valueChanged = new EventEmitter<boolean>();

  public taskForm!: FormGroup;
  public feeEarners$!: User[];
  public skills$!: Skill[];
  public selectedSkills!: Skill[];
  public selectedFeeEarners!: User[]; 
  public taskRequest = {} as AddModifyTaskReq;

  constructor(
    private fb: FormBuilder,
    private userService: UserService) {
    this.taskForm = this.fb.group({
      MATTER: ['', Validators.required],
      FEE_EARNERS: ['', Validators.required],
      TASK_DESCRIPTION: ['', Validators.required],
      SKILLS: ['', Validators.required],
    });
  }
  ngOnInit(): void {
    this.userService.GetUsers().subscribe({
      next: (res:User[]) => {
        this.feeEarners$ = res
        this.feeEarners$.forEach(usr => usr.FirstName = usr.FirstName+' '+usr.LastName)
      }, error: (err) => console.trace(err)
    })
    this.userService.GetSkills().subscribe({
      next: (res:Skill[]) => this.skills$ = res
      , error: (err) => console.trace(err)
    })
  }
  createNewTask(): void {
    console.log(this.diary.DIARY_ID)
    if (this.taskForm.valid) {
      let skills: string[] = [];
      let feeEarners: string[] = [];
      for (let skill of this.selectedSkills) skills.push(skill.skilL_ID.toString());
      for (let FE of this.selectedFeeEarners) feeEarners.push(FE.PFID.toString());
      this.taskRequest.MATTER = this.taskForm.value.MATTER;
      this.taskRequest.DIARY_ID = this.diary.DIARY_ID!;
      this.taskRequest.FEE_EARNERS = feeEarners.join(", ")
      this.taskRequest.SKILLS = skills.join(", ")
      this.taskRequest.TASK_DESCRIPTION = this.taskForm.value.TASK_DESCRIPTION;
      this.taskRequest.SHOW = 'true';
      this.userService.AddDiaryTask(this.taskRequest).subscribe((res:any) => console.log(res));
    }
  }
}

