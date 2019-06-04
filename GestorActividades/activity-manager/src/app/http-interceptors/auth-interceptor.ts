import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { AuthService } from '../auth/auth.service';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()

export class AuthInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService) {}


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.authService.isLoggedIn()) {
            const authToken = this.authService.getAuthorizationToken();
            req = req.clone({
                setHeaders:
                    { Authorization: "Bearer " + authToken }
                }
            );
        }
        
        return next.handle(req).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.authService.logout();
                location.reload(true);
            }
            
            const error = err.error.message || err.statusText;
            return throwError(error);
        }));
    }
}