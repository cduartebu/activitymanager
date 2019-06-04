import { Injectable } from '@angular/core';
import { Activity } from "./activity";
import { Observable, of } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { ResponseDto } from 'src/app/httpResponse';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  url = 'http://localhost:44333/Api/Activity/';  

  constructor(private http: HttpClient) { }

  getActivities() : Observable<ResponseDto<Activity[]>>
  {
    return this.http.get<ResponseDto<Activity[]>>(this.url);
  }

  getActivitiesByUser(userId:number) : Observable<ResponseDto<Activity[]>>
  {
    return this.http.get<ResponseDto<Activity[]>>(this.url +'User/' + userId);
  }

  updateActivity(activityId:number, activity: Activity) : Observable<ResponseDto<Activity>>
  {
    return this.http.put<ResponseDto<Activity>>(this.url + activityId, activity);
  }

  createActivity(activity: Activity) : Observable<ResponseDto<Activity>>
  {
    return this.http.post<ResponseDto<Activity>>(this.url, activity);
  }
}
