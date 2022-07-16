import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-add-product-modal',
  templateUrl: './add-product-modal.component.html',
  styleUrls: ['./add-product-modal.component.css']
})
export class AddProductModalComponent implements OnInit {


  productName: string;
  unitName: string;

  @Input() addNewProduct = new EventEmitter();
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
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

}
