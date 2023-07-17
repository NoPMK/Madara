import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetPropertyModel } from 'src/app/models/get-property-model';
import { PhotoService } from 'src/app/services/photo.service';
import { PropertyService } from 'src/app/services/property.service';
import { CarouselModalService } from '../../services/carousel-modal.service';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css'],
})

export class PropertyDetailsComponent implements OnInit {
public openModalEvent: EventEmitter<any> = new EventEmitter<any>();

  propertyId: number = 0;
  propertyDetails!: GetPropertyModel;
  photos!: string[];
  lat: any;
  long: any;
  address!: string;
  modalIsOpen: boolean = false;
  

  constructor(
    private activatedRoute: ActivatedRoute,
    public propertyService: PropertyService,
    private photoService: PhotoService,
    public carousel: CarouselModalService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((paramMap) => {
      const propertyId: number = Number(paramMap.get('id'));
      if (propertyId) {
        this.propertyId = propertyId;
        this.loadPropertyDetails();
         this.loadPhotos();
      }
    });
  }

  loadPropertyDetails() {
    this.propertyService.getPropertyById(this.propertyId).subscribe({
      next: (data: GetPropertyModel) => (this.propertyDetails = data),
      error: (err) => console.log(err),
    });
  }

  loadPhotos() {
    this.photoService.getAllPhotos(this.propertyId).subscribe({
      next: (data: string[]) => (this.photos = data),
      error: (err) => console.log(err),
    });
  }

  getCurrentPropertyLocation() {
    this.address = `${this.propertyDetails.cityName} ${this.propertyDetails.district} ${this.propertyDetails.street} ${this.propertyDetails.streetNumber}`;
    return this.address;
  }

  openModal(){
    this.carousel.openModal();
  }
}
