import { Injectable } from '@angular/core';
import { Activity } from "./activity";
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  activities: Activity[] = [
    { ActivityId: 1, Title: "Activity 1", Description: "Description Activity 1", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 1 },
    { ActivityId: 2, Title: "Activity 2", Description: "Description Activity 2", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 1 },
    { ActivityId: 3, Title: "Activity 3", Description: "Description Activity 3", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 3 },
    { ActivityId: 4, Title: "Activity 4", Description: "Description Activity 4", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 3 },
    { ActivityId: 5, Title: "Activity 5", Description: "Description Activity 5", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 2 },
    { ActivityId: 6, Title: "Activity 6", Description: "Description Activity 6", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 1 },
    { ActivityId: 7, Title: "Activity 7", Description: "Description Activity 7", StartDate: new Date(), DueDate: new Date(), CreatedDt: new Date(), Status: 3 }
  ];

  constructor() { }

  getActivities() : Observable<Activity[]>
  {
    return of(this.activities);
  }
}
