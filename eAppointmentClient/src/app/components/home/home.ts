import { Component, computed, signal } from '@angular/core';
import { departments } from '../../constants';
import { DepartmentModel, DoctorModel } from '../../models/doctor.model';
import { FormsModule } from '@angular/forms';
import { httpResource } from '@angular/common/http';
import { DxSchedulerModule } from 'devextreme-angular';

@Component({

  selector: 'app-home',
  imports: [
    FormsModule,
    DxSchedulerModule
  ],
  templateUrl: './home.html'

})

export default class Home {

  readonly departmentValues = signal<DepartmentModel[]>(departments)
  readonly doctors = httpResource<ODataResponse<DoctorModel>>(() => "http://localhost:5159/odata/doctors")

  readonly doctorList = computed(() => this.doctors.value()?.value ?? [])

  readonly selectedDepartmentValue = signal<number>(0)
  readonly selectedDoctorId = signal<string>("")

  appointments: any = [

    {
      startDate: new Date("2025-09-29 09:00"),
      endDate: new Date("2025-09-29 10:00"),
      text: "Deniz Karabulut"
    },
    {
      startDate: new Date("2025-09-28 12:00"),
      endDate: new Date("2025-09-28 12:30"),
      text: "Selin Yalçınkaya"
    },
    {
      startDate: new Date("2025-09-29 13:00"),
      endDate: new Date("2025-09-29 14:00"),
      text: "Mertcan Özdemir"
    },
    {
      startDate: new Date("2025-09-30 14:00"),
      endDate: new Date("2025-09-30 15:00"),
      text: "Elif Nur Aksoy"
    }

  ]

}
