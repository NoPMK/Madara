import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  base_url = environment.base_url + '/api/Photo';
  
  constructor(private http: HttpClient) { }

  getAllPhotos(id: number): Observable<string[]> {
    return this.http.get<string[]>(`${this.base_url}/${id}`)
  }
}
