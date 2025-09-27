export interface DoctorModel {

    id: string,
    firstName: string,
    lastName: string,
    identityNumber: string,
    department: number,
    email: string,
    phoneNumber: string,
    city: string,
    country: string,
    street: string,
    createdUserId: string,
    createdUserName: string,
    createdAt: Date | null,
    updatedUserId: string | null,
    updatedUserName: string | null,
    updatedAt: Date | null,
    isDeleted: boolean

}

export const initialDoctor: DoctorModel = {
    id: "",
    firstName: "",
    lastName: "",
    identityNumber: "",
    department: 1,
    email: "",
    phoneNumber: "",
    city: "",
    country: "",
    street: "",
    createdUserId: "",
    createdUserName: "",
    createdAt: new Date(),
    updatedUserId: "",
    updatedUserName: "",
    updatedAt: new Date(),
    isDeleted: false
    
}

export class DepartmentModel{
    name: string = "";
    value: number = 0;
}