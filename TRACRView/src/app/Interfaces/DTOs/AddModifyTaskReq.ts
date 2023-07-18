import { SkillDTO } from "./SkillDTO";

export interface AddModifyTaskReq {
    diaryId: number,
    skills?: SkillDTO[]|null,
    matter: string|null,
    taskDescription?: string|null,
    show?: boolean,
}