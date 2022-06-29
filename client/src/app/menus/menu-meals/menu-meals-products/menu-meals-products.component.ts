import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { MealsService } from 'src/app/_services/meals.service';

@Component({
  selector: 'app-menu-meals-products',
  templateUrl: './menu-meals-products.component.html',
  styleUrls: ['./menu-meals-products.component.css']
})
export class MenuMealsProductsComponent implements OnInit {

  products: Product[];

  constructor(private mealService: MealsService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(){
    this.mealService.getProducts().subscribe((response: any) => {
      this.products = response;
    })
  }

}
