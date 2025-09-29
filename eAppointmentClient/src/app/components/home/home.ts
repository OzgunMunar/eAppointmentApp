import { Component, ElementRef, inject, signal, ViewChild } from '@angular/core';
import { departments } from '../../constants';
import { DepartmentModel, DoctorModel, initialDoctor } from '../../models/doctor.model';
import { FormsModule, NgForm } from '@angular/forms';
import { httpResource } from '@angular/common/http';
import { DxSchedulerComponent, DxSchedulerModule } from 'devextreme-angular';
import { HttpService } from '../../services/httpService';
import { AppointmentModel, initialAppointment } from '../../models/appointment.model';
import { CreateAppointmentModel, initialCreateAppointment } from '../../models/create-appointment.model';
import { DatePipe } from '@angular/common';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientModel } from '../../models/patient.model';
import { FlexiToastService } from 'flexi-toast';

declare const $: any

@Component({

  selector: 'app-home',
  imports: [
    FormsModule,
    DxSchedulerModule,
    FormValidateDirective
  ],
  templateUrl: './home.html',
  providers: [DatePipe]

})

export default class Home {

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined
  @ViewChild(DxSchedulerComponent, { static: false }) scheduler?: DxSchedulerComponent;

  readonly departmentValues = signal<DepartmentModel[]>(departments)
  readonly doctors = httpResource<ODataResponse<DoctorModel>>(() => "http://localhost:5159/odata/doctors")

  readonly doctorList = signal<DoctorModel[]>([])

  readonly selectedDepartmentValue = signal<number>(0)
  readonly selectedDoctorId = signal<string>("")

  readonly #http = inject(HttpService)
  readonly #date = inject(DatePipe)
  readonly #toast = inject(FlexiToastService)

  readonly appointments = signal<AppointmentModel[]>([{ ...initialAppointment }])
  readonly createAppointmentModel = signal<CreateAppointmentModel>({ ...initialCreateAppointment })
  readonly newAppointment = signal<CreateAppointmentModel>({ ...initialCreateAppointment })

  getAllDoctorsByDepartment() {

    if (this.selectedDepartmentValue() > 0) {

      this.#http.getById<DoctorModel[]>('appointments/GetAllDoctorsByDepartment', 'departmentValue',
        this.selectedDepartmentValue(),
        (res) => {

          if (res && res.data) {

            this.doctorList.set(res.data)

          }

        })

    }

  }

  getAllAppointmentsByDoctorId() {

    if (this.selectedDoctorId()) {

      this.#http.getById<AppointmentModel[]>('appointments/GetAllAppointmentsByDoctorId', 'doctorId',
        this.selectedDoctorId(),
        (res) => {

          if (res && res.data) {

            this.appointments.set(res.data)

          }

        })

    }

  }

  getPatientByIdentityNumber() {

    this.#http.getById<PatientModel>('appointments/getPatientByIdentityNumber', 'IdentityNumber',
      this.createAppointmentModel().identityNumber,
      (res) => {

        if (res && res.data) {

          this.createAppointmentModel().firstName = res.data.firstName
          this.createAppointmentModel().lastName = res.data.lastName
          this.createAppointmentModel().identityNumber = res.data.identityNumber
          this.createAppointmentModel().patientId = res.data.id
          this.createAppointmentModel().country = res.data.country
          this.createAppointmentModel().city = res.data.city
          this.createAppointmentModel().street = res.data.street
          this.createAppointmentModel().email = res.data.email
          this.createAppointmentModel().phoneNumber = res.data.phoneNumber

        } else {

          this.createAppointmentModel.update(val => {

            val.patientId = null
            val.firstName = ""
            val.lastName = ""
            val.country = ""
            val.city = ""
            val.street = ""
            val.email = ""
            val.phoneNumber = ""

            return val

          });
        }

      })

  }

  onAppointmentFormOpening(event: any) {

    event.cancel = true

    this.createAppointmentModel().startDate = this.#date.transform(event.appointmentData.startDate, "MM.dd.yyyy HH:mm") ?? ""
    this.createAppointmentModel().endDate = this.#date.transform(event.appointmentData.endDate, "MM.dd.yyyy HH:mm") ?? ""
    this.createAppointmentModel().doctorId = this.selectedDoctorId()

    $("#addModal").modal("show")

  }

  create(form: NgForm) {

    if (form.valid) {

      this.#http.post<CreateAppointmentModel>('appointments', this.createAppointmentModel(), (res) => {

        this.#toast.showToast("Appointment Saved.", "Appointment successfully saved.", "success")
        this.addModalCloseBtn?.nativeElement.click();
        this.createAppointmentModel.set({ ...initialCreateAppointment })
        this.getAllAppointmentsByDoctorId()

      })

    }

  }

  onAppointmentDeleted(event: any) {

    event.cancel = true
    
    this.#toast.showSwal("Delete appointment", `Do you want to delete ${event.appointmentData.patient.fullName}'s appointment?`, "Delete", () => {

      this.#http.delete(`appointments/${event.appointmentData.id}`, (res) => {

        if (res.isSuccessful) {
          
          this.#toast.showToast("Success", `Appointment(${event.appointmentData.patient.fullName}) deleted.`, "success") 
          this.getAllAppointmentsByDoctorId()

        } else {

          this.#toast.showToast("Error", `Appointment(${event.appointmentData.patient.fullName}) could not be deleted`, "error")

        }

      })

    }, "Cancel", () => {
      this.getAllAppointmentsByDoctorId()
    })

  }

  onAppointmentDeleting(event: any) {

    event.cancel = true

  }

}