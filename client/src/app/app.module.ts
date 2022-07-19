import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuMainComponent } from './menus/menu-main/menu-main.component';
import { MenuMealsComponent } from './menus/menu-meals/menu-meals.component';
import { MenuMakeListComponent } from './menus/menu-make-list/menu-make-list.component';
import { MenuMealsProductsComponent } from './menus/menu-meals/menu-meals-products/menu-meals-products.component';
import { MenuMealsMealsComponent } from './menus/menu-meals/menu-meals-meals/menu-meals-meals.component';
import {HttpClientModule} from '@angular/common/http';
import { EditProductModalComponent } from './modals/edit-product/edit-product-modal/edit-product-modal.component';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { AddProductModalComponent } from './modals/edit-product/add-product-modal/add-product-modal.component';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { EditUnitsComponent } from './modals/edit-product/edit-units/edit-units.component';






@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MenuMainComponent,
    MenuMealsComponent,
    MenuMakeListComponent,
    MenuMealsProductsComponent,
    MenuMealsMealsComponent,
    EditProductModalComponent,
    AddProductModalComponent,
    ConfirmDialogComponent,
    EditUnitsComponent,


    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ModalModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
  ],
  providers: [BsModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
