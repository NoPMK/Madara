import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgModel } from '@angular/forms';
import { Router } from '@angular/router';
import { circle, latLng, polygon, tileLayer } from 'leaflet';
import { GetAllPropertiesModel } from 'src/app/models/get-all-properties-model';
import { EnumModel } from 'src/app/models/types/enum-model';
import { UserModel } from 'src/app/models/user-model';
import { AuthService } from 'src/app/services/auth.service';
import { PhotoService } from 'src/app/services/photo.service';
import { PropertyService } from 'src/app/services/property.service';
import { serverErrorHandler } from 'src/app/utils/server-error.handler';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  propertyList!: GetAllPropertiesModel[];
  loaded: boolean = false;
  searchText: string = '';
  p:number = 1;
  itemsPerPage: number = 7;
  totalProperties:any;
  counties: Array<EnumModel> = [];
  districts: Array<EnumModel> = [];
  propertyForm: FormGroup;
  photos!: string[]
  isUserValidToDelete = false;
  userModel!: UserModel;

  constructor(private propertyService: PropertyService, private router: Router, private authService: AuthService, private photoService: PhotoService) {
    this.propertyForm = new FormGroup ({
        county: new FormControl(null),
        cityName: new FormControl(''),
        district: new FormControl(null),
        isForSale:new FormControl('all'),
        minPropertySize: new FormControl(0),
        maxPropertySize: new FormControl(0),
        minNumberOfRooms: new FormControl(0),
        maxNumberOfRooms: new FormControl(0),
        minPrice:new FormControl(0),
        maxPrice:new FormControl(0)
    })
  }

  ngOnInit(): void {
    this.fillForm();
    this.getMe()
    this.checkAuthentication();
  }

  
  getMe() {
    this.authService.getMe().subscribe({
      next: (data: UserModel) =>{ 
        this.userModel = data;
      console.log(this.userModel.userId);
    },
    error: (err) => console.log(err)
  })
}

  checkAuthentication() {
    this.authService.refresh().subscribe();
  }

  openModal (){   
    var modal = document.getElementById("myModal")!;
    var btn = document.getElementById("myBtn");
    var span = document.getElementsByClassName("close")[0];
    
    modal.style.display = "flex";
  }
  
  closeModal() {
    var modal = document.getElementById("myModal")!;
    modal.style.display = "none";
  }

  search() {
    this.propertyService.getAll(this.propertyForm.value).subscribe({
      next: (propertyListData) => {
        this.propertyList = propertyListData;
        this.loaded = true;
        console.log()
        setTimeout(()=> this.isUserValidToDeleteMethod(), 100);
      },
      error: (err) => serverErrorHandler(err, this.propertyForm)
    });
  }
  
  isUserValidToDeleteMethod() {
    for(let property of this.propertyList){
      console.log(property.userId);
      property.isUserValidToEdit = this.isUserValidToDelete;
      for(let role of this.userModel.roles) {
        if(role == 'Admin') {
          property.isUserValidToEdit = true;
        }
      }
      if(this.userModel.userId == property.userId)
      {
        property.isUserValidToEdit = true;
      }
    }
  }

  fillForm() {
    this.propertyService.fetchFormInitData().subscribe({
      next: (response) => {
        this.counties = response.counties;
        this.districts = response.districts;
      },
      error: (err) => {
        console.log(err)
      }
    });
  }
  
  goToEdit(id:number){
    this.router.navigate(['editProperty', id]);
  }

  goToDetails(id: number) {
    this.router.navigate(['propertyDetails', id]);
  }

  onSearchTextInput(searchValue: string){
    this.searchText = searchValue.toLowerCase();
  }
}
