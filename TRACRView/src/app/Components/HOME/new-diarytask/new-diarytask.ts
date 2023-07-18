import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Diary } from 'src/app/Interfaces/Diary';
import { DiaryTask } from 'src/app/Interfaces/DiaryTask';
import { UserService } from 'src/app/Services/UserService/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddModifyTaskReq } from 'src/app/Interfaces/DTOs/AddModifyTaskReq';
import { SkillDTO } from 'src/app/Interfaces/DTOs/SkillDTO';

@Component({
  selector: 'new-diarytask',
  templateUrl: './new-diarytask.html',
  styles: []
})
export class NewDiaryTaskComponent {
  @Input() diary!: Diary;
  @Output() valueChanged = new EventEmitter<boolean>();

  public taskForm!: FormGroup;
  public skills = [] as SkillDTO[];
  public taskRequest = {} as AddModifyTaskReq;

  constructor(
    private fb: FormBuilder,
    private userService: UserService) {
    this.taskForm = this.fb.group({
      matter: ['', Validators.required],
      taskDescription: ['', Validators.required],
      skills: ['', Validators.required],
    });
  }
  createNewTask(): void {
    if (this.taskForm.valid) {
      const skillDTO = {} as SkillDTO;
      [skillDTO.show,skillDTO.colour,skillDTO.skillName] = [true,'blue',this.taskForm.value.skills];
      this.skills.push(skillDTO);
      this.taskRequest.matter = this.taskForm.value.matter;
      this.taskRequest.diaryId = parseInt(this.diary.diaryId);
      this.taskRequest.skills = this.skills;
      this.taskRequest.taskDescription = this.taskForm.value.taskDescription;
      this.taskRequest.show = true;
      this.userService.AddDiaryTask(this.taskRequest).subscribe((res:any) => console.log(res));
    }
  }
}

