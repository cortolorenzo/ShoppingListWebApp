import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';
import { ChangeDetectorRef } from '@angular/core';
import { SchedulesService } from 'src/app/_services/schedules.service';
import { Schedule } from 'src/app/_models/schedule';
import { scheduleParams } from 'src/app/_models/scheduleParams';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { ScheduleRecipe } from 'src/app/_models/scheduleRecipe';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SchedulerAddRecipeComponent } from 'src/app/modals/scheduler-add-recipe/scheduler-add-recipe.component';
import { Recipe } from 'src/app/_models/recipe';
import { NgForm } from '@angular/forms';

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
  bsModalRef: BsModalRef;

  constructor(private schedulesService: SchedulesService, private cdRef:ChangeDetectorRef
    ,private confirmService: ConfirmService,  private toastr: ToastrService
    , private modalService: BsModalService) { }

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
       // console.log(this.schedules)
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

  reloadCurrentSchedule(){
    
    let dateNow = new Date( this.schedules[this.activeSlideIndex].scheduleDate);

    this.scheduleParams.isInitial = true;
    this.scheduleParams.pageSize = 14;
    this.scheduleParams.date = dateNow;
    
    console.log(dateNow)
    this.schedulesService.getSchedule(this.scheduleParams).subscribe((sch: any) => {
      this.schedules = sch;
      console.log(this.schedules)
      this.reload = false;
      //this.dateFormatted();
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

  openAddRecipeModal(){
    const config = {
      class: 'modal-dialog-centered',
      initialState:{}
    }
    this.bsModalRef = this.modalService.show(SchedulerAddRecipeComponent, config);
    this.bsModalRef.content.addSelectedRecipes.subscribe( res  => {
      
      const recipesToAdd : Recipe[] = res;
      const scheduleRecipesToAdd : ScheduleRecipe[] = [];
      var i = recipesToAdd.length
        while (i--){
          // if its already on the list then no need to add it
          let indexFound = this.schedules[this.activeSlideIndex]
          .scheduleRecipes.findIndex(x => x.recipeId == recipesToAdd[i].recipeId);
          
          if (indexFound > -1){
            recipesToAdd.splice(i, 1);
          } 
          else{
            const scheduleRecipeToAdd : ScheduleRecipe = {
              scheduleRecipeId: 0,
              scheduleId: this.schedules[this.activeSlideIndex].scheduleId,
              recipeId: recipesToAdd[i].recipeId,
              quantity: 1,
              recipeName: recipesToAdd[i].recipeName
            }

            scheduleRecipesToAdd.push(scheduleRecipeToAdd);


          }
        }
        
        if(scheduleRecipesToAdd.length > 0){

          scheduleRecipesToAdd.sort((a, b) => (a.recipeId < b.recipeId ? -1 : 1));
          this.schedulesService.addScheduleRecipes(scheduleRecipesToAdd).subscribe(() =>{
            this.reloadSchedule();
            this.toastr.success("Recipes added");
          })
          
        }
        else{
          this.toastr.success("Recipes already on the list");
          return;
        }
    

    })
  }
  updateSchedule(form: NgForm){
    console.log(form);
    this.schedulesService.updateSchedule(this.schedules[this.activeSlideIndex])
              .subscribe(() => {
                form.form.markAsPristine();
                form.form.markAsUntouched();
                
                this.toastr.success("Schedule updated");
              })
  }

  reloadSchedule(){
    
    const params = new scheduleParams();
    params.isInitial = false;
    params.pageSize = 0;
    params.date = new Date(this.schedules[this.activeSlideIndex].scheduleDate);

    console.log(params.date);
this.showCarousel = false;
    this.schedulesService.getSchedule(params).subscribe((schedule: any) => {
      this.schedules[this.activeSlideIndex] = schedule[0];
      console.log(schedule);
      console.log(this.schedules);
      this.showCarousel = true;
    })

  }

}
