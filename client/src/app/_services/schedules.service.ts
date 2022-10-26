import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BsModalService } from 'ngx-bootstrap/modal';
import { environment } from 'src/environments/environment';
import { Schedule } from '../_models/schedule';
import { scheduleParams } from '../_models/scheduleParams';
import { getSchedulesParams } from './common';

@Injectable({
  providedIn: 'root'
})
export class SchedulesService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private modalService: BsModalService) { }


  deleteScheduleRecipe(scheduleRecipeId: number){
    return this.http.delete(this.baseUrl + 'schedules/' + scheduleRecipeId);
    
  }


  getSchedule(sP: scheduleParams){

    //console.log(sP);
    let httpParams = getSchedulesParams(sP.isInitial,sP.loadDirection,sP.pageSize,sP.date)
    //console.log(httpParams);

    return this.http.get<Schedule[]>(this.baseUrl + 'schedules', {params: httpParams});
  }

  formatDate(date: Date){
    return (
      [
        date.getFullYear(),
        this.padTo2Digits(date.getMonth() + 1),
        this.padTo2Digits(date.getDate()),
      ].join('-'))
  }

  padTo2Digits(num: number) {
    return num.toString().padStart(2, '0');
  }


  formatViewDate(dateInput: Date){
    var date  = new Date(dateInput)
    return ( [this.dayToStringDay(date.getDay()), ' (',
      [
        
        date.getFullYear(),
        this.padTo2Digits(date.getMonth() + 1),
        this.padTo2Digits(date.getDate())
      ].join('-'),
      ')'].join('')
      
      )
  }

  dayToStringDay(dayNum: number){
    switch (dayNum) {

      case 0:
        return "Sunday"
      case 1:
        return "Monday"
      case 2:
        return "Tuesday"
      case 3:
        return "Wednesday"
      case 4:
        return "Thursday"
      case 5:
        return "Friday"
      case 6:
        return "Saturday"
    
    }
  }
}
