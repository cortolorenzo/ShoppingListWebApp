import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { MealsService } from 'src/app/_services/meals.service';
import { environment } from 'src/environments/environment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditProductModalComponent } from 'src/app/modals/edit-product/edit-product-modal/edit-product-modal.component';

@Component({
  selector: 'app-menu-meals-products',
  templateUrl: './menu-meals-products.component.html',
  styleUrls: ['./menu-meals-products.component.css']
})
export class MenuMealsProductsComponent implements OnInit {

  products: Product[];
  bsModalRef: BsModalRef;

  constructor(private mealService: MealsService,  private modalService: BsModalService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(){
    this.mealService.getProducts().subscribe((response: any) => {
      this.products = response;
    })

    console.log(environment.apiUrl + 'products');
  }

  openEditProductModal(product: Product){
    const config ={
      class: 'modal-dialog-centered',
      initialState:{ product}
    }

    this.bsModalRef = this.modalService.show(EditProductModalComponent, config);
    this.bsModalRef.content.updateSelectedProduct.subscribe((values: any[]) => {
      console.log(values);

    })
  }

}
