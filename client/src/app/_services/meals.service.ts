import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from '../_models/product';
import { Unit } from '../_models/unit';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { EditUnitsComponent } from '../modals/edit-product/edit-units/edit-units.component';

@Injectable({
  providedIn: 'root'
})
export class MealsService {

  bsModalRef: BsModalRef;
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient, private modalService: BsModalService) { }

  editUnitsOpenModal(units: Unit[]) : Observable<boolean> {
    const config ={
      class: 'modal-lm',
      initialState : {units}
    }
    this.bsModalRef = this.modalService.show(EditUnitsComponent,config);
    return new Observable<boolean>(this.getResultEditUnitsModal())

  }


  private getResultEditUnitsModal(){
    return (observer: any) => {
      const subscription = this.bsModalRef.onHide.subscribe(() => {
        observer.next(this.bsModalRef.content.result);
        observer.complete();
      });

      return {
        unsubscribe() {
          subscription.unsubscribe()
        }
        
      }
    }
  }



  getProducts(){
    return this.http.get<Product[]>(this.baseUrl + 'products');
    
  }

  getUnits(){
    return this.http.get<Unit>(this.baseUrl + 'units');
    
  }

  addProduct(product: Product){
    console.log("nowy produkt")
    return this.http.post<Product>(this.baseUrl + 'products', product);
  }

  addUnit(unitName: string){
    console.log(unitName)
    return this.http.post(this.baseUrl + 'units/' + unitName, unitName);
  }


  updateProduct(product: Product){

    console.log("jestem");
    return this.http.put(this.baseUrl + 'products', product)
   
  }

  deleteProduct(productId: number){
    console.log("kasuję: "+ this.baseUrl + 'products/' + productId);
    return this.http.delete(this.baseUrl + 'products/' + productId);
  }


  deleteUnit(unitName: string){
    return this.http.delete(this.baseUrl + 'units/' + unitName)
  }

}
