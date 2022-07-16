import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { MealsService } from 'src/app/_services/meals.service';
import { environment } from 'src/environments/environment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditProductModalComponent } from 'src/app/modals/edit-product/edit-product-modal/edit-product-modal.component';
import { AddProductModalComponent } from 'src/app/modals/edit-product/add-product-modal/add-product-modal.component';

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

  deleteProduct(productId: number){
    this.mealService.deleteProduct(productId).subscribe(() => {
      this.products.splice(this.products.findIndex(x => x.productId === productId),1);
    });
  }


  openAddProductModal(){
    const config ={
      class: 'modal-dialog-centered'
      
    }
  
    this.bsModalRef = this.modalService.show(AddProductModalComponent, config);
    this.bsModalRef.content.addNewProduct.subscribe((values: any[]) => {
      if (values.find(x => x.name == "cancelled").value == 1)
        return;
      else
      {
        const newProduct: Product = {
          productId:  0,
          productName: values.find(x => x.name == "productName").value,
          unitName: values.find(x => x.name == "unitName").value
        }

        this.mealService.addProduct(newProduct).subscribe(() =>{
          this.loadProducts();
        });
        console.log(newProduct);
      }
    })
    
    }

  openEditProductModal(product: Product){
    const config ={
      class: 'modal-dialog-centered',
      initialState:{ product}
    }
    const tempProduct: Product = product; 

    this.bsModalRef = this.modalService.show(EditProductModalComponent, config);
    this.bsModalRef.content.updateSelectedProduct.subscribe((values: any[]) => {
      
      if (values.find(x => x.name == "canceled").value == 1)
        product = tempProduct;
      else
      {
        product.productName = values.find(x => x.name == "productName").value;
        this.mealService.updateProduct(product).subscribe();
        console.log(product);
      }
        
    })
  }

}
