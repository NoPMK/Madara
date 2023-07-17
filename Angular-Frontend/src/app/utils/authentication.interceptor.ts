import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, finalize } from "rxjs";
import { LoaderService } from "../services/loader.service";

@Injectable({
    providedIn: 'root'
})

export class AuthenticationInterceptor implements HttpInterceptor {

constructor (public loaderService: LoaderService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        if(localStorage.getItem('accessToken')){
            this.loaderService.isLoading.next(true);
            const newRequest = req.clone({
                headers: req.headers.set('Authorization',`Bearer ${localStorage.getItem('accessToken')}`)
            })

            return next.handle(newRequest);
        }

        return next.handle(req).pipe(
            finalize(
                () => {
                    this.loaderService.isLoading.next(false);
                }
            )
        );
    }

}