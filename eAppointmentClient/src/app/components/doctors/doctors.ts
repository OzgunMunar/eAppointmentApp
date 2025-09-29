import { Component, computed, effect, ElementRef, inject, signal, ViewChild, viewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FlexiGridModule } from 'flexi-grid';
import { httpResource } from '@angular/common/http';
import { DepartmentModel, DoctorModel, initialDoctor } from '../../models/doctor.model';
import { departments } from '../../constants';
import * as bootstrap from 'bootstrap';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpService } from '../../services/httpService';
import { FlexiToastService } from 'flexi-toast';
import { FormValidateDirective } from 'form-validate-angular';

@Component({
  selector: 'app-doctors',
  imports: [
    RouterLink,
    FlexiGridModule,
    FormsModule,
    FormValidateDirective
  ],
  templateUrl: './doctors.html'
})

export default class Doctors {
  
  readonly newDoctor = signal<DoctorModel>({ ...initialDoctor })
  readonly updateDoctorValues = signal<DoctorModel>({ ...initialDoctor })
  readonly updateDoctorId = signal<string>("")
  readonly departmentList = signal<DepartmentModel[]>(departments)

  @ViewChild('addFirstInput') addFirstInput!: ElementRef<HTMLInputElement>
  @ViewChild('updateFirstInput') updateFirstInput!: ElementRef<HTMLInputElement>
  @ViewChild('addModal') addModalRef!: ElementRef<HTMLDivElement>;
  @ViewChild('updateModal') updateModalRef!: ElementRef<HTMLDivElement>;

  readonly #toastr = inject(FlexiToastService)
  readonly #http = inject(HttpService)

  readonly doctors = httpResource<ODataResponse<DoctorModel>>(() => "http://localhost:5159/odata/doctors")

  readonly data = computed(() => {
    return this.doctors.value()?.value.map((val, i) => {

      const dept: DepartmentModel = departments.find(d => d.value === val.department)!;

      return {

        ...val,
        index: i + 1,
        departmentId: dept.value,
        departmentName: dept.name,
        fullName: `${val.firstName} ${val.lastName}`

      } as DoctorModel
    }) ?? []
  })

  readonly loading = computed(() => this.doctors.isLoading())

  openAddModal() {

    this.newDoctor.set({ ...initialDoctor })

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

  saveDoctor(form: NgForm) {

    if (!form.valid) {

      this.#toastr.showToast("Missing Data", "There are empty fields!", "error")
      return

    }

    this.newDoctor.set(form.value)

    this.#http.post('doctors', this.newDoctor(), (res) => {

      this.#toastr.showToast("Doctor Saved", "Doctor successfully saved.")

      const modalInstance = bootstrap.Modal.getInstance(this.addModalRef.nativeElement)
      modalInstance?.hide()

      this.doctors.reload()

    })

  }

  deleteDoctor(doctorId: string, doctorName: string) {

    this.#toastr.showSwal("Delete Doctor?", `Are you sure that you want to delete ${doctorName}?`, "Delete", () => {

      this.#http.delete(`doctors/${doctorId}`, (res) => {

        if (res.isSuccessful) {

          this.#toastr.showToast("Success", `Doctor(${doctorName}) deleted`, "success")
          this.doctors.reload()

        } else {

          this.#toastr.showToast("Error", `Doctor(${doctorName}) could not be deleted`, "error")
          
        }

      })

    }, "Cancel")

  }

  updateDoctor(form: NgForm) {

    this.#http.put(`doctors/${this.updateDoctorId()}`, this.updateDoctorValues(), (res) => {

      this.#toastr.showToast("Doctor Updated", "Doctor successfully updated.");

      const modalInstance = bootstrap.Modal.getInstance(this.updateModalRef.nativeElement)
      modalInstance?.hide()

      this.doctors.reload()

    })


  }

  getValuesForUpdate(id: string) {

    const doctor = this.doctors.value()?.value.find(doc => doc.id == id)

    if (!doctor) {

      this.#toastr.showToast("Problem", "There is a problem with fetching data.", "error")
      return

    }
    this.updateDoctorValues.set({ ...doctor })
    this.updateDoctorId.set(id)

  }

}