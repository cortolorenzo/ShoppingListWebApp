import { Component, EventEmitter, Input, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/_models/product';
import { MealsService } from 'src/app/_services/meals.service';

@Component({
  selector: 'app-add-recipe-product',
  templateUrl: './add-recipe-product.component.html',
  styleUrls: ['./add-recipe-product.component.css']
})
export class AddRecipeProductComponent implements OnInit {

  @Input() addSelectedProducts = new EventEmitter();
  products: any[];

  constructor(public bsModalRef: BsModalRef, private mealService: MealsService) { }

  ngOnInit(): void {
    this.getProducts();
    
  }

  addProducts(){
    //console.log(this.products)
    const productsToAdd = this.products.filter(el => el.checked === true);
    //console.log(productsToAdd);
    this.addSelectedProducts.emit(productsToAdd);
    this.bsModalRef.hide();
  }

  getProducts(){
    if (!this.products){
      this.mealService.getProducts().subscribe((response: any) => {
        
        console.log(response);
        this.products = response;

        return this.products;
      })
     }
     return this.products;
  }

}
