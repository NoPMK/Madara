import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EnumModel } from 'src/app/models/types/enum-model';
import { AuthService } from 'src/app/services/auth.service';
import { PropertyService } from 'src/app/services/property.service';
import { serverErrorHandler } from 'src/app/utils/server-error.handler';
import { CreatePropertyModel } from '../../models/create-property-model';

@Component({
  selector: 'app-create-property',
  templateUrl: './create-property.component.html',
  styleUrls: ['./create-property.component.css'],
})
export class CreatePropertyComponent implements OnInit {
  address: string = '';
  counties: Array<EnumModel> = [];
  districts: Array<EnumModel> = [];
  createPropertyForm: FormGroup;
  propertyTypes!: Array<EnumModel>;
  heatTypes: Array<EnumModel> = [];
  floorTypes: Array<EnumModel> = [];
  comfortTypes: Array<EnumModel> = [];
  conditionTypes: Array<EnumModel> = [];
  parkingTypes: Array<EnumModel> = [];
  onNext = false;
  isCheckedAirConditionered: boolean = false;
  isCheckedHandicapped: boolean = false;
  userId: number | string | undefined;

  constructor(
    private router: Router,
    private propertyService: PropertyService,
    private authService: AuthService
  ) {
    this.authService.user.subscribe({
      next: (user) => {
        this.userId = user?.userId;
      },
    });

    this.createPropertyForm = new FormGroup({
      county: new FormControl(null, Validators.required),
      cityName: new FormControl('', Validators.required),
      district: new FormControl(null),
      street: new FormControl('', Validators.required),
      streetNumber: new FormControl(1, [Validators.required, Validators.min(1)]),
      photos: new FormControl(''),
      isForSale: new FormControl(null, Validators.required),
      type: new FormControl(null, Validators.required),
      propertySize: new FormControl(0, [Validators.required, Validators.min(0)]),
      groundSize: new FormControl(0, [Validators.required, Validators.min(0)]),
      description: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(10000)]),
      numberOfRooms: new FormControl(0, [Validators.required, Validators.min(0)]),
      numberOfHalfRooms: new FormControl(0),
      price: new FormControl(0, [Validators.required, Validators.min(0)]),
      condition: new FormControl(null, Validators.required),
      heat: new FormControl(null, Validators.required),
      yearOfBuild: new FormControl(0, [Validators.required, Validators.min(0), Validators.max(2028)]),
      numberOfFloors: new FormControl(null, Validators.required),
      comfort: new FormControl(null, Validators.required),
      isHandicapped: new FormControl(false),
      isAirConditioned: new FormControl(false),
      parking: new FormControl(null, Validators.required),
      createdAt: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.fillForm();
    this.checkAuthentication();
  }

  fillForm() {
    this.propertyService.fetchFormInitData().subscribe({
      next: (response) => {
        this.comfortTypes = response.comforts;
        this.conditionTypes = response.conditions;
        this.counties = response.counties;
        this.districts = response.districts;
        this.floorTypes = response.floors;
        this.heatTypes = response.heats;
        this.parkingTypes = response.parkings;
        this.propertyTypes = response.properties;
      },
      error: (err) => {
        console.log(err);
        if (err.status === 400) {
          serverErrorHandler(err, this.createPropertyForm);
        }
      },
    });
  }

  checkAuthentication() {
    this.authService.refresh();
  }

  next() {
    this.onNext = true;
  }

  back() {
    this.onNext = false;
  }

  save() {
    this.convertBools();
    console.log(this.createPropertyForm.value);
    console.log(this.userId);

    const formValues: CreatePropertyModel = this.createPropertyForm.value;

    this.propertyService.createProperty(formValues).subscribe({
      next: () => {
        this.router.navigate(['home']);
      },
      error: (err) => {
        serverErrorHandler(err, this.createPropertyForm), console.log(err);
      },
    });
  }

  convertBools() {
    var sold = 
      this.createPropertyForm.value.isSold === 'true' ? true : false;
    var sale =
      this.createPropertyForm.value.isForSale === 'true' ? true : false;
    var air =
      this.createPropertyForm.value.isAirConditioned == 'true' ? true : false;
    var handicap =
      this.createPropertyForm.value.isHandicapped == 'false' ? true : false;
    var deleted =
      this.createPropertyForm.value.isDeleted === 'true' ? true : false;

    this.createPropertyForm.value.isSold = sold;
    this.createPropertyForm.value.isForSale = sale;
    this.createPropertyForm.value.isAirConditioned = air;
    this.createPropertyForm.value.isHandicapped = handicap;
    this.createPropertyForm.value.isDeleted = deleted;
  }
}
