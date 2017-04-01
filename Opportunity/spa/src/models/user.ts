export interface IUser {
        id: number;
        firstName: string;
        familyName: string;
        email: string;
        profileUrl: string;
        photoUrl: string;
        hasOpenApplications: boolean;
        hasOpenOpportunities: boolean;
        canManageOpportunities: boolean;
}