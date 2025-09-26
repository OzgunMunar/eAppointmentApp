import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FlexiGridModule } from 'flexi-grid';

@Component({
  selector: 'app-doctors',
  imports: [
    RouterLink,
    FlexiGridModule
  ],
  templateUrl: './doctors.html'
})

export default class Doctors {



}
