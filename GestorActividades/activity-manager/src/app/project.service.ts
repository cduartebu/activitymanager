import { Injectable } from '@angular/core';
import { Project } from './project';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  projects: Project[] = [
    { id: 1, name: 'First Project', description: "This is the description of a project", endDate:new Date(), startDate:new Date() },
    { id: 2, name: 'Second Project', description: "This is the description of a project", endDate:new Date(), startDate:new Date() },
    { id: 3, name: 'Third Project', description: "This is the description of a project", endDate:new Date(), startDate:new Date() },
    { id: 4, name: 'Fourth Project', description: "This is the description of a project", endDate:new Date(), startDate:new Date() }
  ];

  constructor() { }

  getProjects() : Observable<Project[]>
  {
      return of(this.projects);
  }
}