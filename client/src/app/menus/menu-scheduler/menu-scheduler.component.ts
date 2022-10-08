import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';
import { ChangeDetectorRef } from '@angular/core';
import { SchedulesService } from 'src/app/_services/schedules.service';
import { Schedule } from 'src/app/_models/schedule';

@Component({
  selector: 'app-menu-scheduler',
  templateUrl: './menu-scheduler.component.html',
  styleUrls: ['./menu-scheduler.component.css'],
  providers: [
    { provide: CarouselConfig, 
      useValue: { interval: false, showIndicators: true} }]
      
})
export class MenuSchedulerComponent implements OnInit {

  schedule: Schedule;
  dateHeader: string;
  constructor(private schedulesService: SchedulesService, private cdRef:ChangeDetectorRef) { }

  ngAfterViewChecked()
  {
    this.cdRef.detectChanges();
  }

  private _activeSlideIndex: number = 0;

  get activeSlideIndex(): number {
    return this._activeSlideIndex;
  };

  set activeSlideIndex(newIndex: number) {
    console.log('Active slider index would be changed!' + newIndex);
    console.log('stary index' + this._activeSlideIndex);
    const direction = newIndex - this._activeSlideIndex;
    if(direction == -2 || direction == 1) {
      this.getSchedule(1);
    }
    else if(direction !== 0){
      this.getSchedule(-1);
    }
   
    this._activeSlideIndex = newIndex;
  };


  ngOnInit(): void {
    this.getScheduleToday();


  }

  getSchedule(addDay: number){
    
    let date = new Date(this.schedule.scheduleDate);
    date.setDate(date.getDate() + addDay)

    //console.log(date)
    this.schedulesService.getSchedule(date).subscribe((sch: any) => {
      if(sch){
        this.schedule = sch;
        console.log(this.schedule)
        this.dateFormatted();
      }
  
    })
  }

  getScheduleToday(){
    
    let dateNow = new Date();
    
    console.log(dateNow)
    this.schedulesService.getSchedule(dateNow).subscribe((sch: any) => {
      this.schedule = sch;
      console.log(this.schedule)
      this.dateFormatted();
    })
  }

  dateFormatted(){
    this.dateHeader = this.schedulesService.formatViewDate(this.schedule.scheduleDate);
    console.log(this.dateHeader);
    
  }

}
