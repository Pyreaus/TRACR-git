import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styles: [`
    .nav-link:hover:not(.text-light) {color: #f8f9faa0 !important;}
    .btn-s-dark {background-color: #2d2e31bc !important;}
    .nav-title {color: hsl(0, 0%, 75%);}
  `]
})
export class NAVBARComponent {
  @Output() rightBarVisibleChange = new EventEmitter<boolean>();

  onNewTaskClick(): void {
    this.rightBarVisibleChange.emit(true);
    console.log('New Task')
  }
}
