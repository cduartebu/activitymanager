import { Component, OnInit,ViewChild } from '@angular/core';
import { Activity } from '../services/activity/activity';
import { ActivityService } from '../services/activity/activity.service';
import { element } from '@angular/core/src/render3';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent implements OnInit {

  activitiesToDo: Activity[] = [];

  activitiesInProgress: Activity[] = [];

  activitiesDone: Activity[] = [];

  
  constructor(private activityService: ActivityService) { }

  ngOnInit() {
    this.getActivities();
  }

  getActivities() : void
  {

    this.activityService.getActivities().subscribe(response => {

      if (response.StatusCode == 1) {

        this.activitiesToDo=[];
        this.activitiesInProgress=[];
        this.activitiesDone=[];

        response.Data.forEach(activity => {

          activity.isDue=new Date(activity.DueDate)<=new Date();
         
          if (activity.Status == 1) {
            this.activitiesToDo.push(activity);
          } else if (activity.Status == 2) {
            this.activitiesInProgress.push(activity);
          } else {
            this.activitiesDone.push(activity);
          }  
        }); 
      }      
    });
  }
  

}