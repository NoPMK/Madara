import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CarouselModalService } from '../../services/carousel-modal.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-carousel-modal',
  templateUrl: './carousel-modal.component.html',
  styleUrls: ['./carousel-modal.component.css'],
})
export class CarouselModalComponent implements OnInit {

  @Output() open: EventEmitter<any> = new EventEmitter();
  @Output() close: EventEmitter<any> = new EventEmitter();

  constructor(public carousel: CarouselModalService) {}

  ngOnInit(): void {}

  closeModal() {
    this.carousel.visible = false;
    this.open.emit(this.carousel.visible);
    // this.carousel.closeModal();
  }

  openModal() {
    this.carousel.visible = true;
    this.close.emit(this.carousel.visible);
    // this.carousel.openModal();
  }
}
