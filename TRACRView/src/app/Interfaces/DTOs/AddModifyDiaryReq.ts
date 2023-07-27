export interface AddModifyDiaryReq {
    PFID: string|null,
    WEEK_BEGINNING: string|null,
    LEARNING_POINTS: string|null,
    PRACTICE_AREA: string|null,
    PROFESSIONAL_DEVELOPMENT_UNDERTAKEN: string|null,
    PROFESSIONAL_CONDUCT_ISSUES?: string|null,
    SIGN_OFF_SUBMITTED: string|null,
    SIGNED_OFF_BY?: string|null,
    SHOW?: string|null
}