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

  schedules: Schedule[];
  dateHeader: string;
  reload: boolean = true;
  showCarousel: boolean = true;
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
    //console.log('nowy index ' + newIndex);
    //console.log('stary index ' + this._activeSlideIndex);
    const direction = newIndex - this._activeSlideIndex;
    if(direction == -13 ) {
      this.getSchedule(1);
    }
    else if(direction == 13){
      this.getSchedule(-1);
    }

   if(this.reload){
  
    this._activeSlideIndex = 7;
    this.reload = false;
   }
   else{
    this._activeSlideIndex = newIndex;
   }
   
  };


  ngOnInit(): void {
    this.getScheduleToday();


  }

  getSchedule(addDay: number){
    var date;
    if (addDay == -1){
      date = new Date(this.schedules[0].scheduleDate);
      date.setDate(date.getDate() + addDay)
    } else{
      date = new Date(this.schedules[13].scheduleDate);
      date.setDate(date.getDate() + addDay)
    }

    this.showCarousel = false;
    //console.log(date)
    this.schedulesService.getSchedule(date).subscribe((sch: any) => {
      if(sch){
        this.schedules = sch;
        //console.log(this.schedules)
        this.reload = true;
        this.showCarousel = true;
        
      }
  
    })
  }

  getScheduleToday(){
    
    let dateNow = new Date();
    
    console.log(dateNow)
    this.schedulesService.getSchedule(dateNow).subscribe((sch: any) => {
      this.schedules = sch;
      console.log(this.schedules)
      this.reload = true;
      //this.dateFormatted();
    })
  }

  dateFormatted(date: Date){
    return this.schedulesService.formatViewDate(date);
    // console.log(this.dateHeader);
    
  }

}
