export interface PatientModel {

    id: string,
    firstName: string,
    lastName: string,
    identityNumber: string,
    email: string,
    phoneNumber: string,
    city: string,
    country: string,
    street: string,

}

export const initialPatient: PatientModel = {

    id: "",
    firstName: "",
    lastName: "",
    identityNumber: "",
    email: "",
    phoneNumber: "",
    city: "",
    country: "",
    street: "",
    
}