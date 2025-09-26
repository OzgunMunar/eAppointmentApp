import { Component, computed, effect } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FlexiGridModule } from 'flexi-grid';
import { httpResource } from '@angular/common/http';
import { DoctorModel } from '../../models/doctor.model';

@Component({
  selector: 'app-doctors',
  imports: [RouterLink, FlexiGridModule],
  templateUrl: './doctors.html'
})
export default class Doctors {

  readonly doctors = httpResource<ODataResponse<DoctorModel>>(() => "http://localhost:5159/odata/doctors");
  
  readonly data = computed<DoctorModel[]>(() => {
  
    return this.doctors.value()?.value.map(value => ({
    
      ...value,
      fullName: `${value.firstName} ${value.lastName}`

    })) ?? [];

  });

  readonly loading = computed(() => this.doctors.isLoading())
 
}
