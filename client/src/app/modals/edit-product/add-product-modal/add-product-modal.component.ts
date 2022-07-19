import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/_models/product';
import { Unit } from 'src/app/_models/unit';
import { MealsService } from 'src/app/_services/meals.service';

@Component({
  selector: 'app-add-product-modal',
  templateUrl: './add-product-modal.component.html',
  styleUrls: ['./add-product-modal.component.css']
})
export class AddProductModalComponent implements OnInit {


  productName: string;
  unitName: string;
  units: Unit[];

  @Input() addNewProduct = new EventEmitter();
  constructor(public bsModalRef: BsModalRef, private mealService: MealsService) { }

  ngOnInit(): void {

    
  }

  updateUnitName(uniName: string){
    this.unitName = uniName
  }

  

  addProduct(cancelled: boolean){

    const product: any = 
    [
      {name: 'productName', value: this.productName},
      {name: 'unitName', value: this.unitName},
      {name: 'cancelled', value: cancelled ? 1 : 0}
      
    ];

    this.addNewProduct.emit(product);
    this.bsModalRef.hide();
  }

  openEditUnitsModal(units: Unit[]){
    this.mealService.editUnitsOpenModal(units).subscribe(res =>{
      if(res){
        console.log("deleted");
        
      }
    })
  }



}
