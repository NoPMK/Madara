import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterModel } from '../models/register-model';
import { environment } from 'src/environments/environment';
import { ConfirmEmailModel } from '../models/confirm-email-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  base_url = environment.base_url + '/api/Users';

  constructor(private http: HttpClient) {}

  registerUser(registerForm: RegisterModel): Observable<any> {
    return this.http.post<any>(`${this.base_url}/Register`, registerForm);
  }

  confirmEmail(confirmEmail: ConfirmEmailModel): Observable<any>{
    return this.http.post<any>(`${this.base_url}/confirmEmail`, confirmEmail);
  }

  LoginWithFacebook(credentials: string): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.http.post(this.base_url + "/LoginWithFacebook", JSON.stringify(credentials), { headers: header, withCredentials: true });
  }

}
