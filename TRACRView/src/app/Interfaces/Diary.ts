export interface Diary {
    pfid: number|null,
    diaryId: string,
    weekBeginning: string,
    learningPoints?: string|null,
    professionalDevelopmentUndertaken?: string|null,
    professionalConductIssues?: string|null,
    signOffSubmitted: boolean,
    signedOffBy?: string|null,
    show: boolean
}