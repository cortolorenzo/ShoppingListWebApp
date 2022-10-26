import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';
import { ChangeDetectorRef } from '@angular/core';
import { SchedulesService } from 'src/app/_services/schedules.service';
import { Schedule } from 'src/app/_models/schedule';
import { scheduleParams } from 'src/app/_models/scheduleParams';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { ScheduleRecipe } from 'src/app/_models/scheduleRecipe';
import { ToastrService } from 'ngx-toastr';

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

  scheduleParams: scheduleParams;
  constructor(private schedulesService: SchedulesService, private cdRef:ChangeDetectorRef
    ,private confirmService: ConfirmService,  private toastr: ToastrService) { }

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
    if(direction < -1 ) {
      this.scheduleParams.isInitial = false;
      this.scheduleParams.loadDirection = 1;
      this.getSchedule(1);
    }
    else if(direction > 2){
      this.scheduleParams.isInitial = false;
      this.scheduleParams.loadDirection = -1;
      this.getSchedule(-1);
    }

   if(this.reload){

      if(this.scheduleParams.isInitial)
        this._activeSlideIndex = this.scheduleParams.pageSize/2;
      else
      {
     
        if (this.scheduleParams.loadDirection == 1)
          this._activeSlideIndex = this.schedules.length - this.scheduleParams.pageSize/2;
        else
          this._activeSlideIndex = this.scheduleParams.pageSize/2 - 1;
      }

    this.reload = false;
   }
   else{
    this._activeSlideIndex = newIndex;
   }
   
  };


  ngOnInit(): void {
    this.scheduleParams = new scheduleParams();
    this.scheduleParams.pageSize = 14;
    this.getScheduleToday();


  }

  getSchedule(addDay: number){
    var date;
    if (addDay == -1){
      date = new Date(this.schedules[0].scheduleDate);
      //date.setDate(date.getDate() + addDay)
      this.scheduleParams.date = date;
    } else{
      date = new Date(this.schedules[this.schedules.length - 1].scheduleDate);
      date.setDate(date.getDate() + addDay)
      this.scheduleParams.date = date;
    }

    this.showCarousel = false;
    //console.log(date)
    this.schedulesService.getSchedule(this.scheduleParams).subscribe((sch: any) => {
      if(sch){
        this.schedules = addDay == 1 ? this.schedules.concat(sch) : sch.concat(this.schedules) ;
        //console.log(this.schedules)
        this.reload = true;
        this.showCarousel = true;
        
      }
  
    })
  }

  deleteScheduleRecipe(scheduleRecipe: ScheduleRecipe){

    this.confirmService.confirm('Schedule recipe delete', 'Are you sure you want to delete: ' + scheduleRecipe.recipeName).subscribe(result => {
      if(result) {
        this.schedulesService.deleteScheduleRecipe(scheduleRecipe.scheduleRecipeId).subscribe(() => {
          this.schedules[this.activeSlideIndex].scheduleRecipes
            .splice(this.schedules[this.activeSlideIndex].scheduleRecipes.findIndex(x => x.scheduleRecipeId === scheduleRecipe.scheduleRecipeId),1);
          this.toastr.success("Product deleted!");
        });

      }
    })
  }

  getScheduleToday(){
    
    let dateNow = new Date();

    this.scheduleParams.isInitial = true;
    this.scheduleParams.pageSize = 14;
    this.scheduleParams.date = dateNow;
    
    console.log(dateNow)
    this.schedulesService.getSchedule(this.scheduleParams).subscribe((sch: any) => {
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
