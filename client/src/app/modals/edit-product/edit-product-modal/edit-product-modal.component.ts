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
  @Input() updateSelectedProduct = new EventEmitter();

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }


  updateProduct(){
    const product: any = 
    [
      {name: 'productName', value: this.product.productName},
      {name: 'unitName', value: this.product.unitName}
    ];

    this.updateSelectedProduct.emit(product);
    this.bsModalRef.hide();


  }


}
