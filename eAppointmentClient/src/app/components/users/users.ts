import { Component, computed, effect, ElementRef, inject, signal, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FlexiGridModule } from 'flexi-grid';
import { httpResource } from '@angular/common/http';
import * as bootstrap from 'bootstrap';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpService } from '../../services/httpService';
import { FlexiToastService } from 'flexi-toast';
import { FormValidateDirective } from 'form-validate-angular';
import { initialUser, UserModel } from '../../models/usermodel';
import { RoleModel } from '../../models/rolemodel';

@Component({
  selector: 'app-users',
  imports: [

    RouterLink,
    FlexiGridModule,
    FormsModule,
    FormValidateDirective

  ],
  templateUrl: './users.html'
})

export default class Users {

  constructor() {

    effect(() => {

      console.log(this.userRoleList())

    })

  }
  
  readonly newUser = signal<UserModel>({ ...initialUser })
  readonly updateUserValues = signal<UserModel>({ ...initialUser })
  readonly updateUserId = signal<string>("")

  @ViewChild('addFirstInput') addFirstInput!: ElementRef<HTMLInputElement>
  @ViewChild('updateFirstInput') updateFirstInput!: ElementRef<HTMLInputElement>
  @ViewChild('addModal') addModalRef!: ElementRef<HTMLDivElement>
  @ViewChild('updateModal') updateModalRef!: ElementRef<HTMLDivElement>

  readonly #toastr = inject(FlexiToastService)
  readonly #http = inject(HttpService)

  readonly users = httpResource<ODataResponse<UserModel>>(() => "http://localhost:5159/odata/users")
  readonly roles = httpResource<ODataResponse<RoleModel>>(() => "http://localhost:5159/odata/roles")
  readonly userRoleList = computed<RoleModel[]>(() => this.roles.value()?.value ?? [])


  readonly data = computed(() => this.users.value()?.value.map((val) => ({

    ...val,
    fullName: `${val.firstName} ${val.lastName}`

  })) ?? [])

  readonly loading = computed(() => this.users.isLoading())

  openAddModal() {

    this.newUser.set({ ...initialUser })

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

  saveUser(form: NgForm) {

    if (!form.valid) {

      this.#toastr.showToast("Missing Data", "There are empty fields!", "error")
      return

    }

    this.newUser.set(form.value)

    this.#http.post('users', this.newUser(), (res) => {

      this.#toastr.showToast("User Saved", "User successfully saved.")

      const modalInstance = bootstrap.Modal.getInstance(this.addModalRef.nativeElement)
      modalInstance?.hide()

      this.users.reload()

    })

  }

  deleteUser(userId: string, userName: string) {

    this.#toastr.showSwal("Delete User?", `Are you sure that you want to delete ${userName}?`, "Delete", () => {

      this.#http.delete(`doctors/${userId}`, (res) => {

        if (res.isSuccessful) {

          this.#toastr.showToast("Success", `Doctor(${userName}) deleted`, "success")
          this.users.reload()

        } else {

          this.#toastr.showToast("Error", `User(${userName}) could not be deleted`, "error")
          
        }

      })

    }, "Cancel")

  }

  updateUser(form: NgForm) {

    this.#http.put(`doctors/${this.updateUserId()}`, this.updateUserValues(), (res) => {

      this.#toastr.showToast("User Updated", "User successfully updated.");

      const modalInstance = bootstrap.Modal.getInstance(this.updateModalRef.nativeElement)
      modalInstance?.hide()

      this.users.reload()

    })

  }

  getValuesForUpdate(id: string) {

    const user = this.users.value()?.value.find(user => user.id == id)

    if (!user) {

      this.#toastr.showToast("Problem", "There is a problem with fetching data.", "error")
      return

    }
    this.updateUserValues.set({ ...user })
    this.updateUserId.set(id)

  }

}