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
}
