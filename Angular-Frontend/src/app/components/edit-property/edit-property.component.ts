import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GetPropertyModel } from 'src/app/models/get-property-model';
import { EnumModel } from 'src/app/models/types/enum-model';
import { UserModel } from 'src/app/models/user-model';
import { AuthService } from 'src/app/services/auth.service';
import { PropertyService } from 'src/app/services/property.service';
import { serverErrorHandler } from 'src/app/utils/server-error.handler';

@Component({
  selector: 'app-edit-property',
  templateUrl: './edit-property.component.html',
  styleUrls: ['./edit-property.component.css']
})
export class EditPropertyComponent implements OnInit {

  editPropertyForm!: FormGroup;
  comfortTypes: Array<EnumModel> = [];
  conditionTypes: Array<EnumModel> = [];
  parkingTypes: Array<EnumModel> = [];
  heatTypes: Array<EnumModel> = [];
  propertyDetails!: GetPropertyModel;
  id!: number;
  userModel!: UserModel;
  isUserValidToDelete = false;
  


  constructor(
    private propertyService: PropertyService,
    private router: Router, private authService: AuthService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {

    this.getId(this.id);
    this.initForm();
    this.fillFormOptions();
    this.getMe()
    setTimeout(()=> this.isUserValidToDeleteMethod(), 100);
    this.checkAuthentication();
    
    setTimeout(()=> console.log(this.userModel), 200);
  }

  initForm() {
    this.editPropertyForm = new FormGroup({
      price: new FormControl(0),
      description: new FormControl(''),
      photos: new FormControl(''),
      isForSale: new FormControl(false),
      propertySize: new FormControl(0),
      numberOfRooms: new FormControl(0),
      numberOfHalfRooms: new FormControl(0),
      condition: new FormControl(null),
      heat: new FormControl(null),
      comfort: new FormControl(null),
      isAirConditionered: new FormControl(false),
      parking: new FormControl(null),
    })
  }

  deleteProperty() {
      this.propertyService.deleteProperty(this.id).subscribe({
        next: () => {
          this.router.navigate(['home'])
        },
        error: (error) => console.log(error),
      });
  }

  isUserValidToDeleteMethod() {
      for(let role of this.userModel.roles) {
        if(role == 'Admin') {
          this.isUserValidToDelete = true;
        }
      }
      if(this.userModel.userId == this.propertyDetails.userId){
        this.isUserValidToDelete = true;
      }
  }

  getMe() {
      this.authService.getMe().subscribe({
        next: (data: UserModel) =>{ 
        this.userModel = data;
        },
        error: (err) => console.log(err)
      })
  }

  save() {
   this.convertBools();
   this.id= this.getId(this.propertyDetails.id);
    this.propertyService.editProperty( this.editPropertyForm.value,this.id).subscribe({
      next: () => {
        this.router.navigate(['home'])
      },
      error: (err) => {
        serverErrorHandler(err, this.editPropertyForm),
          console.log(err)
      }
    })
  }

  backToList() {
    this.router.navigate(['home']);
  }




  fillFormOptions() {
    this.propertyService.fetchFormInitData().subscribe({
      next: (response) => {
        this.comfortTypes = response.comforts;
        this.conditionTypes = response.conditions;
        this.heatTypes = response.heats;
        this.parkingTypes = response.parkings;
      },
      error: (err) => {
        console.log(err);
        if (err.status === 400) {
          serverErrorHandler(err, this.editPropertyForm);
        }
      }
    });
    this.loadPropertyDetails();

  }
  checkAuthentication() {
    this.authService.refresh().subscribe();
  }

  loadPropertyDetails() {
    this.propertyService.getPropertyById(this.id).subscribe({
      next: (data: GetPropertyModel) =>{ 
      this.propertyDetails = data;
      this.fillFormData();
      
      },
      error: (err) => console.log(err)
    })
  }

  getId(id:number) {
    this.activatedRoute.paramMap.subscribe(
      paramMap => {
        const propertyId: number = Number(paramMap.get('id'));
        if (propertyId) {
          this.id = propertyId;
        }
      }
    )
    return this.id;
  }
  fillFormData() {
    this.editPropertyForm.get('description')?.setValue(this.propertyDetails.description);
    this.editPropertyForm.get('price')?.setValue(this.propertyDetails.price);
    this.editPropertyForm.get('isForSale')?.setValue(this.propertyDetails.isForSale);
    this.editPropertyForm.get('propertySize')?.setValue(this.propertyDetails.propertySize);
    this.editPropertyForm.get('numberOfRooms')?.setValue(this.propertyDetails.numberOfRooms);
    this.editPropertyForm.get('numberOfHalfRooms')?.setValue(this.propertyDetails.numberOfHalfRooms);
    this.editPropertyForm.get('condition')?.setValue(this.propertyDetails.condition);
    this.editPropertyForm.get('heat')?.setValue(this.propertyDetails.heat);
    this.editPropertyForm.get('comfort')?.setValue(this.propertyDetails.comfort);
    this.editPropertyForm.get('isAirConditionered')?.setValue(this.propertyDetails.isAirConditionered);
    this.editPropertyForm.get('parking')?.setValue(this.propertyDetails.parking);

  }
  convertBools() {
    var sold = this.editPropertyForm.value.isSold === 'true' ? true : false;
    var sale = this.editPropertyForm.value.isForSale === 'true' ? true : false;
    var air =  this.editPropertyForm.value.isAirConditioned === 'true' ? true : false;
    var handicap = this.editPropertyForm.value.isHandicapped === 'true' ? true : false;
    var deleted = this.editPropertyForm.value.isDeleted === 'true' ? true : false;

    this.editPropertyForm.value.isSold = sold;
    this.editPropertyForm.value.isForSale = sale;
    this.editPropertyForm.value.isAirConditioned = air;
    this.editPropertyForm.value.isHandicapped = handicap;
    this.editPropertyForm.value.isDeleted = deleted;
  }
}
