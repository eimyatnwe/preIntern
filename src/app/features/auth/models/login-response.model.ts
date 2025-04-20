export interface LoginResponse {
    email: string;
    token: string;
    roles: string[];
    memberId: string;
}