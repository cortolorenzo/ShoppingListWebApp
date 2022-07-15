import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/_models/product';


@Component({
  selector: 'app-edit-product-modal',
  templateUrl: './edit-product-modal.component.html',
  styleUrls: ['./edit-product-modal.component.css']
})
export class EditProductModalComponent implements OnInit {

  product: Product;

  productName: string;
  productId: number;
  @Input() updateSelectedProduct = new EventEmitter();

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
    this.productName = this.product.productName

  }




  updateProduct(canceled: boolean){
    const product: any = 
    [
      {name: 'productName', value: this.productName},
      // {name: 'unitName', value: this.product.unitName}
      {name: 'productId', value: this.product.productId},
      {name: 'canceled', value: canceled ? 1 : 0}
      
    ];

    this.updateSelectedProduct.emit(product);
    this.bsModalRef.hide();


  }


}
