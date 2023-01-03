import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/_models/product';
import { Unit } from 'src/app/_models/unit';
import { MealsService } from 'src/app/_services/meals.service';


@Component({
  selector: 'app-edit-product-modal',
  templateUrl: './edit-product-modal.component.html',
  styleUrls: ['./edit-product-modal.component.css']
})
export class EditProductModalComponent implements OnInit {

  product: Product;
  units: Unit[];
  unitName: string;

  productName: string;
  productId: number;
  @Input() updateSelectedProduct = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private mealService: MealsService) { }

  ngOnInit(): void {
    this.productName = this.product.productName
    this.unitName = this.product.unitName

  }


  updateUnitName(unitName: string){
    this.unitName = unitName
  }


  updateProduct(canceled: boolean){
    const product: any = 
    [
      {name: 'productName', value: this.productName},
      {name: 'unitName', value: this.unitName},
      {name: 'productId', value: this.product.productId},
      {name: 'canceled', value: canceled ? 1 : 0}
      
    ];

    this.updateSelectedProduct.emit(product);
    this.bsModalRef.hide();


  }

  openEditUnitsModal(units: Unit[]){
    this.mealService.editUnitsOpenModal(units).subscribe(res =>{
      if(res){
     //   console.log("deleted");
        //this.loadUnits();
      }
    })
  }

 


}
