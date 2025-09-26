export class DoctorModel {
    id: string = "";
    firstName: string = "";
    lastName: string = "";
    identityNumber: string = "";
    department: number = 0;
    email: string = "";
    phoneNumber: string = "";
    city: string = "";
    country: string = "";
    street: string = "";
    createdUserId: string = "";
    createdUserName: string = "";
    createdAt: Date | null = null;
    updatedUserId: string | null = null;
    updatedUserName: string | null = null;
    updatedAt: Date | null = null;
    isDeleted: boolean = false;
}