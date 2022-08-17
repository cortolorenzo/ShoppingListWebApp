import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Unit } from 'src/app/_models/unit';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { MealsService } from 'src/app/_services/meals.service';

@Component({
  selector: 'app-edit-units',
  templateUrl: './edit-units.component.html',
  styleUrls: ['./edit-units.component.css']
})
export class EditUnitsComponent implements OnInit {

  result: boolean = false;
  units: Unit[];
  newUnitName: string;
  newUnit: Unit;
  constructor(private mealService: MealsService,
                 private confirmService: ConfirmService
                 ,public bsModalRef: BsModalRef) { }

  ngOnInit(): void {

  }


  deleteUnit(unit: Unit){

    this.confirmService.confirm('Unit delete', 'Are you sure you want to delete: ' + unit.unitName).subscribe(result => {
      if(result){
        
        this.mealService.deleteUnit(unit.unitName).subscribe(res => {
         
          this.units.splice(this.units.findIndex(x => x.unitId === unit.unitId),1);
          this.result = true;
          
        })
      }
    })
   
  }

  addUnit()
  {
    if ( this.newUnitName || this.newUnitName.length > 0) 
    {
      this.mealService.addUnit(this.newUnitName).subscribe((res: any) => {

        this.newUnit = res;
        this.units.push(this.newUnit);

        console.log(res);
        
        this.newUnitName = "";

    });
    }
    
  }

  decline(){
    this.bsModalRef.hide();

  }

}
