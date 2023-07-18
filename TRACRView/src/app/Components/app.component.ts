import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  template: `
  <app-navbar></app-navbar>
  <router-outlet></router-outlet>`,
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private primengConfig: PrimeNGConfig) {}
  title = 'TRACR';
  ngOnInit() {
    this.primengConfig.zIndex = {
        modal: 0,    // dialog, sidebar
        // overlay: 1000,  // dropdown, overlaypanel
        // menu: 1000,     // overlay menus
        // tooltip: 1100   // tooltip
    };
  }
}
