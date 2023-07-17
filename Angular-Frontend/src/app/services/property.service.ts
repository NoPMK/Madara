import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreatePropertyModel } from '../models/create-property-model';
import { Observable } from 'rxjs';
import { FormInitdataModel } from '../models/form-init-data-model';
import { environment } from 'src/environments/environment';
import { GetCityModel } from '../models/get-city-model';
import { GetPropertyModel } from '../models/get-property-model';
import { formatDate } from '@angular/common';
import { EditPropertyModel } from '../models/edit-property-model';
import { DetailedSearchModel } from '../models/detailed-search-model';
import { GeocodeAddressModel } from '../models/geocode-address-model';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {


  base_url = environment.base_url + '/api/Properties';

  constructor(private http: HttpClient) { }

  fullProperty: Array<CreatePropertyModel> = [];

  createProperty(property: CreatePropertyModel): Observable<any>{
    return this.http.post(this.base_url, property)
  }

  fetchFormInitData(): Observable<FormInitdataModel> {
    return this.http.get<FormInitdataModel>(`${this.base_url}/FormData`);
  }

  getAll(model: DetailedSearchModel): Observable<any> {
    return this.http.post(`${this.base_url}/search`, model);
  }

  getCityById(id: number): Observable<GetCityModel> {
    return this.http.get<GetCityModel>(`${this.base_url}/GetCity/${id}`)
  }

  getPropertyById(id: number): Observable<GetPropertyModel> {
    return this.http.get<GetPropertyModel>(`${this.base_url}/${id}`)
  }

  formatDate(date: string): string {
    return formatDate(date, 'yyyy-MM-dd', 'en');
  }
  editProperty( editProperty: EditPropertyModel,id:number): Observable<any> {
    return this.http.put<any>(`${this.base_url}/${id}`,editProperty)
  }
  getAllPhotos(id: number): Observable<string[]> {
    return this.http.get<string[]>(`${this.base_url}/${id}`)
  }
  deleteProperty(id: number): Observable<any> {
    return this.http.delete(`${this.base_url}/${id}`)
  }
}
