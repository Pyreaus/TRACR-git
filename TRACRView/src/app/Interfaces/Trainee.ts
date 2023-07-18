export interface Trainee {
    traineeId?: number|null,
    firstName: string,
    lastName: string,
    photo?: string,
    email?: string|null,
    telephone?: string|null,
    traineePfid: number,
    reviewerPfid?: number|null,
    active?: boolean
}