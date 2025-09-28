import { Component, computed, effect, ElementRef, inject, signal, ViewChild, viewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FlexiGridModule } from 'flexi-grid';
import { httpResource } from '@angular/common/http';
import { PatientModel, initialPatient } from '../../models/patient.model';
import * as bootstrap from 'bootstrap';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpService } from '../../services/httpService';
import { FlexiToastService } from 'flexi-toast';
import { FormValidateDirective } from 'form-validate-angular';

@Component({
  selector: 'app-patients',
  imports: [
    RouterLink,
    FlexiGridModule,
    FormsModule,
    FormValidateDirective
  ],
  templateUrl: './patients.html'
})

export default class Patients {

  readonly newPatient = signal<PatientModel>({ ...initialPatient })
  readonly updatePatientValues = signal<PatientModel>({ ...initialPatient })
  readonly updatePatientId = signal<string>("")

  @ViewChild('addFirstInput') addFirstInput!: ElementRef<HTMLInputElement>
  @ViewChild('updateFirstInput') updateFirstInput!: ElementRef<HTMLInputElement>
  @ViewChild('addModal') addModalRef!: ElementRef<HTMLDivElement>;
  @ViewChild('updateModal') updateModalRef!: ElementRef<HTMLDivElement>;

  readonly #toastr = inject(FlexiToastService)
  readonly #http = inject(HttpService)

  readonly patients = httpResource<ODataResponse<PatientModel>>(() => "http://localhost:5159/odata/patients")

  readonly data = computed(() => {
    return this.patients.value()?.value ?? []
  })

  readonly loading = computed(() => this.patients.isLoading())

  openAddModal() {

    this.newPatient.set({ ...initialPatient })

    const modalEl = this.addModalRef.nativeElement
    const modal = new bootstrap.Modal(modalEl)

    modalEl.addEventListener('shown.bs.modal', () => {
      this.addFirstInput?.nativeElement.focus()
    }, { once: true })

    modal.show()

  }

  openUpdateModal(id: string) {

    const modalEl = this.updateModalRef.nativeElement
    const modal = new bootstrap.Modal(modalEl)

    modalEl.addEventListener('shown.bs.modal', () => {
      this.updateFirstInput?.nativeElement.focus()
    }, { once: true })

    this.getValuesForUpdate(id)

    modal.show()

  }

  savePatient(form: NgForm) {

    if (!form.valid) {

      this.#toastr.showToast("Missing Data", "There are empty fields!", "error")
      return

    }

    this.newPatient.set(form.value)

    this.#http.post('patients', this.newPatient(), (res) => {

      this.#toastr.showToast("Patient Saved", "Patient successfully saved.")

      const modalInstance = bootstrap.Modal.getInstance(this.addModalRef.nativeElement)
      modalInstance?.hide()

      this.patients.reload()

    })

  }

  deletePatient(patientId: string, patientName: string) {

    this.#toastr.showSwal("Delete Patient?", `Are you sure that you want to delete ${patientName}?`, "Delete", () => {

      this.#http.delete(`patients/${patientId}`, (res) => {

        if (res.isSuccessful) {
          
          this.#toastr.showToast("Success", `Doctor(${patientName}) deleted`, "success")
          this.patients.reload();

        } else {
          this.#toastr.showToast("Error", `Doctor(${patientName}) could not be deleted`, "error")
        }

      })

    }, "Cancel")

  }

  updatePatient(form: NgForm) {

    this.#http.put(`patients/${this.updatePatientId()}`, this.updatePatientValues(), (res) => {

      this.#toastr.showToast("Patient Updated", "Patient successfully updated.");

      const modalInstance = bootstrap.Modal.getInstance(this.updateModalRef.nativeElement)
      modalInstance?.hide()

      this.patients.reload()

    })

  }

  getValuesForUpdate(id: string) {

    const patient = this.patients.value()?.value.find(pat => pat.id == id)

    if (!patient) {

      this.#toastr.showToast("Problem", "There is a problem with fetching data.", "error")
      return

    }

    this.updatePatientValues.set({ ...patient })
    this.updatePatientId.set(id)

  }

}