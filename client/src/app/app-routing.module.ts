import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuMainComponent } from './menus/menu-main/menu-main.component';
import { MenuMealsComponent } from './menus/menu-meals/menu-meals.component';

const routes: Routes = [
  {path: '', component: MenuMainComponent},
  {path: 'meals', component: MenuMealsComponent}



];









@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
