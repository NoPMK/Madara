import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CarouselModalService {

  visible: boolean = false;
  constructor() {}

  openModal() {
    var modal = document.getElementById('carouselModal')!;
    var btn = document.getElementById('myBtn');
    var span = document.getElementsByClassName('close')[0];

    this.visible = true;
    modal.style.display = 'block';
  }

  closeModal() {
    var modal = document.getElementById('carouselModal')!;
    this.visible = false;
    modal.style.display = 'none';
  }
}
