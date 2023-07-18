export interface AddModifyDiaryReq {
    pfid: number,
    weekBeginning: string,
    LearningPoints?: string|null,
    ProfessionalDevelopmentUndertaken?: string|null,
    ProfessionalConductIssues?: string|null,
    SignOffSubmitted: boolean,
    signedOffBy?: string|null,
    show?: boolean
}