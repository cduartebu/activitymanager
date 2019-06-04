import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { throwError, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  serverUrl = 'https://activitymanager2.azurewebsites.net/';
  errorData: {};

  constructor(private http: HttpClient) { }

  redirectUrl: string;

  login(username: string, password: string) {

    const body = new HttpParams()
    .set('grant_type', "password")          
    .set('username', username)    
    .set('password', password);

    return this.http.post<any>(`${this.serverUrl}token`, body.toString(), {observe: 'response', headers: { 'Content-Type': 'application/x-www-form-urlencoded' }})
    .pipe(map(res => {
        if (res.status === 200) { 
          if (res.body && res.body.access_token) {
            localStorage.setItem('currentUser', JSON.stringify(res.body));
          }
        }
      }),
      catchError(this.handleError)
    );
  }

  isLoggedIn() {
    if (localStorage.getItem('currentUser')) {
      return true;
    }
    return false;
  }

  getAuthorizationToken() {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    return currentUser.access_token;
  }

  logout() {
    localStorage.removeItem('currentUser');
  }

  private handleError(error: HttpErrorResponse) {

    if (error.status == 400) {
      return of([] as any);
    }

    if (error.error instanceof ErrorEvent) {

      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {

      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(`Backend returned code ${error.status}, ` + `body was: ${error.error}`);
              
    }

    // return an observable with a user-facing error message
    this.errorData = {
      errorTitle: 'Oops! Request for document failed',
      errorDesc: 'Something bad happened. Please try again later.'
    };
    return throwError(this.errorData);
  } 
}
