import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './navbar/navbar';

@Component({
  selector: 'app-layouts',
  imports: [
    RouterOutlet,
    Navbar
  ],
  templateUrl: './layouts.html'
})

export default class Layouts {

}
