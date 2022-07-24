import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuMainComponent } from './menus/menu-main/menu-main.component';
import { MenuMealsComponent } from './menus/menu-meals/menu-meals.component';
import { MenuMakeListComponent } from './menus/menu-make-list/menu-make-list.component';
import { MenuMealsProductsComponent } from './menus/menu-meals/menu-meals-products/menu-meals-products.component';
import { MenuMealsRecipiesComponent } from './menus/menu-meals/menu-meals-recipies/menu-meals-recipies.component';

const routes: Routes = [
  {path: '', component: MenuMainComponent},
  {path: 'meals'   , component: MenuMealsComponent},
  {path: 'makelist', component: MenuMakeListComponent},
  {path: 'products', component: MenuMealsProductsComponent},
  {path: 'recipies', component: MenuMealsRecipiesComponent}



];









@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
