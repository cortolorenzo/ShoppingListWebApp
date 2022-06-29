import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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






@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MenuMainComponent,
    MenuMealsComponent,
    MenuMakeListComponent,
    MenuMealsProductsComponent,
    MenuMealsMealsComponent,

    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
