import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';  
import { User } from '../services/user/user';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  dataSaved = false;    
  message: string = "";
  errorMessage: string = "";
  userForm: FormGroup;
  userId: number = 0;
  allUsers: User[]; 

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.initUserForm();
    this.getUsers();
  }

  get username() { return this.userForm.get('username'); }
  get lastName() { return this.userForm.get('lastName'); }
  get password() { return this.userForm.get('password'); }
  get emailAddress() { return this.userForm.get('emailAddress'); }
  get firstName() { return this.userForm.get('firstName'); }

  initUserForm()
  {
    this.userForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      emailAddress: ['', [Validators.required, Validators.email]]
    });
  }

  getUsers()    
  {      
    this.userService.getUsers().subscribe(response => {
      if (response.StatusCode == 1) {
        this.allUsers = response.Data;
      }  
    });
  }

  addUser(user: User) {  
    user.UserId = this.userId;
    this.errorMessage = "";
    this.message = "";
    this.userService.addUser(user).subscribe(
        (response) => {  
          if (response.StatusCode == 1) {
            this.dataSaved = true;
            this.message = 'User saved Successfully';
            this.getUsers();
            this.reset();  
            this.userId = 0;
          }
          else {
            this.errorMessage = response.StatusMessage;
          }  
        })
  }         

  reset()    
  {    
    this.userForm.reset();    
  } 

  deleteUser(userId: number) {  
    if (confirm("Are you sure you want to delete this user?")) {
      this.userService.deleteUser(userId).subscribe(  
          (response) => {  
            if (response.StatusCode == 1) {
              this.dataSaved = true;  
              this.message = "Deleted Successfully";
              this.getUsers(); 
            }                
          });  
    }  
  } 
}