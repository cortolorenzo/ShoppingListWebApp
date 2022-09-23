import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { MealsService } from 'src/app/_services/meals.service';
import { environment } from 'src/environments/environment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditProductModalComponent } from 'src/app/modals/edit-product/edit-product-modal/edit-product-modal.component';
import { AddProductModalComponent } from 'src/app/modals/edit-product/add-product-modal/add-product-modal.component';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { Unit } from 'src/app/_models/unit';
import { map, take } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-menu-meals-products',
  templateUrl: './menu-meals-products.component.html',
  styleUrls: ['./menu-meals-products.component.css']
})
export class MenuMealsProductsComponent implements OnInit {

  products: Product[];
  units: Unit[];
  bsModalRef: BsModalRef;

  constructor(private mealService: MealsService,  private modalService: BsModalService
    ,private confirmService: ConfirmService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadUnits();
  }

  loadProducts(){
    this.mealService.getProducts().subscribe((response: any) => {
      
      console.log(response);
      this.products = response;
    })

    // this.mealService.getProducts().pipe(map((response: Product[]) => {
    //    console.log(response)
    //   this.products = response;
    // })).subscribe();

    console.log(environment.apiUrl + 'products');
  }


  loadUnits(){
    this.mealService.getUnits().subscribe((response: any) => {
      
      this.units = response;
      
    })

  }



  deleteProduct(productId: number, productName: string){

    this.confirmService.confirm('Product delete', 'Are you sure you want to delete: ' + productName).subscribe(result => {
      if(result) {
        this.mealService.deleteProduct(productId).subscribe(() => {
          this.products.splice(this.products.findIndex(x => x.productId === productId),1);
          this.toastr.success("Product deleted!");
        });

      }
    })
    
  }


  openAddProductModal(){
    const config ={
      class: 'modal-dialog-centered modal-ms',
      
      
    }
    
    this.bsModalRef = this.modalService.show(AddProductModalComponent, config);
    this.bsModalRef.content.units = this.units;
    this.bsModalRef.content.addNewProduct.subscribe((values: any[]) => {
      // if (values.find(x => x.name == "cancelled").value == 1)
      //   return;
      // else
      {
        const newProduct: Product = {
          productId:  0,
          productName: values.find(x => x.name == "productName").value,
          unitName: values.find(x => x.name == "unitName").value
        }

        this.mealService.addProduct(newProduct).subscribe(() =>{
          this.loadProducts();
          this.loadUnits();
          this.toastr.success("New product added!");
          

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
    this.bsModalRef.content.units = this.units;
    this.bsModalRef.content.updateSelectedProduct.subscribe((values: any[]) => {
      
      if (values.find(x => x.name == "canceled").value == 1)
        product = tempProduct;
      else
      {
        product.productName = values.find(x => x.name == "productName").value;
        product.unitName = values.find(x => x.name == "unitName").value;
        this.mealService.updateProduct(product).subscribe(() =>{
          this.toastr.success("Product updated!");
        });
      
        console.log(product);
      }
        
    })
  }

}
