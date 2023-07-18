import { Skill } from "./Skill";

export interface DiaryTask {
    diaryTaskId: string,
    diaryId: string,
    skills?: Skill[]|null,
    matter?: string|null,
    taskDescription?: string|null,
    Show?: boolean|null,
}