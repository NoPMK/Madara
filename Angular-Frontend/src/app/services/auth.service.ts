import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UserModel } from '../models/user-model';
import { environment } from 'src/environments/environment';


interface TokenDataModel {
  accessToken: {
    value: string;
    expiration: string;
  },
  refreshToken: {
    value: string;
    expiration: string;
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  base_url = environment.base_url + '/api/Users';

  user = new BehaviorSubject<UserModel | null>(null)
  
  constructor(private http: HttpClient ) { }

  login(loginData: {userName: string; password: string;}): Observable<TokenDataModel>{

    return this.http.post<TokenDataModel>(`${this.base_url}/Login`, loginData)
    .pipe(
     tap((tokenData)=> {
 
       if(tokenData.accessToken ){
         localStorage.setItem('accessToken', tokenData.accessToken.value);
         localStorage.setItem('refreshToken', tokenData.refreshToken.value);

        this.getMe().subscribe()
       }
     })
    )
   }

   

   refresh(): Observable<TokenDataModel>{
    const refreshToken = localStorage.getItem('refreshToken') ? localStorage.getItem('refreshToken') : '';
    return this.http.post<TokenDataModel>(`${this.base_url}/Refresh`, {refreshToken})
    .pipe(
      tap((tokenData)=> {

        if(tokenData.accessToken && tokenData.refreshToken){
          localStorage.setItem('accessToken', tokenData.accessToken.value);
          localStorage.setItem('refreshToken', tokenData.refreshToken.value);

          this.getMe().subscribe()
        }
      })
    )
  }

  getMe(): Observable<UserModel>{
    return this.http.get<UserModel>(`${this.base_url}/Me`)
    .pipe(
      tap(userModel => {
        this.user.next(userModel)
      })
    )
  }

  logout(){
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');

    this.user.next(null)
  }
}

