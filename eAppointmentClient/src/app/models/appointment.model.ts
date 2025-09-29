import { initialPatient, PatientModel } from "./patient.model"

export interface AppointmentModel {

    id: string,
    startDate: string,
    endDate: string,
    text: string,
    patient: PatientModel

}

export const initialAppointment: AppointmentModel = {
    
    id: "",
    startDate: "",
    endDate: "",
    text: "",
    patient: { ...initialPatient }
    
}