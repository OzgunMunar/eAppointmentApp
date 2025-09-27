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

  readonly newDoctor = signal<DoctorModel>({...initialDoctor})
  readonly departmentList = signal<DepartmentModel[]>(departments)
  @ViewChild('firstInput') firstInput!: ElementRef<HTMLInputElement>
  @ViewChild('addModal') addModalRef!: ElementRef<HTMLDivElement>;

  readonly #toastr = inject(FlexiToastService)
  readonly #http = inject(HttpService)

  readonly doctors = httpResource<ODataResponse<DoctorModel>>(() => "http://localhost:5159/odata/doctors");

  readonly data = computed(() => {
    return this.doctors.value()?.value.map((val, i) => {

      const dept: DepartmentModel = departments.find(d => d.value === val.department)!;

      return {

        ...val,
        index: i + 1,
        departmentId: dept.value,
        departmentName: dept.name,
        fullName: `${val.firstName} ${val.lastName}`

      } as DoctorModel;
    }) ?? [];
  });

  readonly loading = computed(() => this.doctors.isLoading())

  openAddModal() {

    this.newDoctor.set({...initialDoctor})

    const modalEl = this.addModalRef.nativeElement;
    const modal = new bootstrap.Modal(modalEl);

    modalEl.addEventListener('shown.bs.modal', () => {
      this.firstInput?.nativeElement.focus();
    }, { once: true });

    modal.show();

  }

  saveDoctor(form: NgForm) {

    if(!form.valid) {

      this.#toastr.showToast("Missing Data", "There are empty fields!","error")
      return

    }

    this.newDoctor.set(form.value)

    console.log(this.newDoctor)

    this.#http.post('doctors', this.newDoctor(), (res) => {

      this.#toastr.showToast("Doctor Saved", "Doctor successfully saved.")
      
      const modalInstance = bootstrap.Modal.getInstance(this.addModalRef.nativeElement);
      modalInstance?.hide();
      
      this.doctors.reload()

    })

  }

}