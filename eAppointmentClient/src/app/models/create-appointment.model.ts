export interface CreateAppointmentModel {
    
    id: string,
    startDate: string,
    endDate: string,
    doctorId: string,
    patientId: string | null,
    firstName: string,
    lastName: string,
    identityNumber: string,
    country: string,
    city: string,
    street: string,
    email: string,
    phoneNumber: string
    
}

export const initialCreateAppointment: CreateAppointmentModel = {
    
    id: "",
    startDate: "",
    endDate: "",
    doctorId: "",
    patientId: null,
    firstName: "",
    lastName: "",
    identityNumber: "",
    country: "",
    city: "",
    street: "",
    email: "",
    phoneNumber: ""

}