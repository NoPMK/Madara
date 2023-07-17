import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhotoUploadComponent } from './material-components/photo-upload/photo-upload.component';
import { MatIconModule } from '@angular/material/icon';
import { InputComponent } from './formFields/input/input.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import SelectComponent from './formFields/select/select.component';
import { MapComponent } from './map/map/map.component';


@NgModule({
  declarations: [
    PhotoUploadComponent,
    InputComponent,
    SelectComponent,
    MapComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule
  ],
  exports: [
    PhotoUploadComponent,
    InputComponent,
    SelectComponent,
    MapComponent
  ]
})
export class SharedModule { }
