import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class MealsService {


  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getProducts(){
    return this.http.get<Product>(this.baseUrl + 'products');
    
  }

  updateProduct(product: Product){

    console.log("jestem");
    return this.http.put(this.baseUrl + 'products', product).subscribe();
   
  }

  deleteProduct(productId: number){
    console.log("kasuję: "+ this.baseUrl + 'products/' + productId);
    return this.http.delete(this.baseUrl + 'products/' + productId);
  }

}
