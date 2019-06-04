import { Component, OnInit, Input } from '@angular/core';
import { Activity } from '../services/activity/activity';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivityService } from '../services/activity/activity.service';
import { DashboardComponent } from '../dashboard/dashboard.component';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

  @Input() activities: Activity[];

  selectedActivity: Activity;

  closeResult: string;

  activityForm: FormGroup;

  dataSaved = false;
  message: string = "";
  errorMessage: string = "";


  constructor(private modalService: NgbModal, private formBuilder: FormBuilder, private activityService: ActivityService, private dashboard: DashboardComponent) { }

  ngOnInit() {



  }

  open(content, activity: Activity) {
    this.selectedActivity = activity;
    this.activityForm = this.formBuilder.group({
      Title: [this.selectedActivity.Title, Validators.required],
      Description: [this.selectedActivity.Description, Validators.required],
      StartDate: [this.selectedActivity.StartDate, Validators.required],
      DueDate: [this.selectedActivity.DueDate, Validators.required],
      Status: [this.selectedActivity.Status, Validators.required]
    });
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  create(content) {

    this.selectedActivity = new Activity();
    this.selectedActivity.Title = "";
    this.selectedActivity.Description = "";
    this.selectedActivity.StartDate = new Date();
    this.selectedActivity.DueDate = new Date();
    this.selectedActivity.Status = 1;

    this.activityForm = this.formBuilder.group({
      Title: ["", Validators.required],
      Description: ["", Validators.required],
      StartDate: [new Date(), Validators.required],
      DueDate: [new Date(), Validators.required],
      Status: [1, Validators.required]
    });
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }


  saveActivity(activity: Activity) {


    activity.ActivityId = this.selectedActivity.ActivityId;
    activity.Status = this.selectedActivity.Status;
    activity.TeamId = this.selectedActivity.TeamId;
    activity.CreatedDt = this.selectedActivity.CreatedDt;

    if (typeof activity.StartDate !== "string" && !(activity.StartDate instanceof Date)) {
      var startDate: any = activity.StartDate;
      activity.StartDate = new Date(startDate.year, startDate.month - 1, startDate.day);
    }
    if (typeof activity.DueDate !== "string" && !(activity.DueDate instanceof Date)) {
      var dueDate: any = activity.DueDate;
      activity.DueDate = new Date(dueDate.year, dueDate.month - 1, dueDate.day);
    }
    if (this.selectedActivity.ActivityId) {
      this.activityService.updateActivity(activity.ActivityId, activity).subscribe(
        (response) => {
          if (response.StatusCode == 1) {
            this.dataSaved = true;
            this.errorMessage = "";
            this.message = 'Activity saved Successfully';
            this.modalService.dismissAll();
            this.dashboard.getActivities();

          } else {
            this.message = "";
            this.errorMessage = response.StatusMessage;
          }
        },
        (response) => {
          this.message = "";
          this.errorMessage = response.error.StatusMessage;

        });
    } else {

      this.activityService.createActivity(activity).subscribe(
        (response) => {
          if (response.StatusCode == 1) {
            this.dataSaved = true;
            this.errorMessage = "";
            this.message = 'Activity created Successfully';
            this.modalService.dismissAll();
            this.dashboard.getActivities();

          } else {
            this.message = "";
            this.errorMessage = response.StatusMessage;
          }
        },
        (response) => {
          this.message = "";
          this.errorMessage = response.error.StatusMessage;

        });

    }
  }

  closeModal() {
    this.modalService.dismissAll();
  }
  toDoStatus() {
    this.selectedActivity.Status = 1;
  }

  inProgressStatus() {
    this.selectedActivity.Status = 2;
  }

  doneStatus() {
    this.selectedActivity.Status = 3;
  }

  deleteActivity(activityId: number) {  
    if (confirm("Are you sure you want to delete this Activity?")) {
      this.activityService.deleteActivity(activityId).subscribe(  
          (response) => {  
            if (response.StatusCode == 1) {
              this.dataSaved = true;  
              this.message = "Deleted Successfully";
              this.modalService.dismissAll();
              this.dashboard.getActivities();
            }                
          });  
    }  
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

}
