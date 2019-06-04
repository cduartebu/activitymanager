import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';  
import { User } from './user';
import { ResponseDto } from 'src/app/httpResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  url = 'https://activitymanager2.azurewebsites.net/Api/User/';  

  constructor(private http: HttpClient) { }

  getUsers() : Observable<ResponseDto<User[]>>
  {    
    return this.http.get<ResponseDto<User[]>>(this.url);
  }

  addUser(user:User):Observable<ResponseDto<User>>
  {
    return this.http.post<ResponseDto<User>>(this.url, user)
  }

  deleteUser(userId:number):Observable<ResponseDto<boolean>>    
  {    
    return this.http.delete<ResponseDto<boolean>>(this.url + userId);
  }

  getUserById(userId: string): Observable<ResponseDto<User>> {    
    return this.http.get<ResponseDto<User>>(this.url + userId);    
  }
}