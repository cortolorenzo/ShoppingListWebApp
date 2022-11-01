import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuMainComponent } from './menus/menu-main/menu-main.component';
import { MenuMealsComponent } from './menus/menu-meals/menu-meals.component';
import { MenuMakeListComponent } from './menus/menu-make-list/menu-make-list.component';
import { MenuMealsProductsComponent } from './menus/menu-meals/menu-meals-products/menu-meals-products.component';
import { MenuMealsRecipesComponent } from './menus/menu-meals/menu-meals-recipes/menu-meals-recipes.component';
import { RecipeEditComponent } from './menus/menu-meals/menu-meals-recipes/recipe-edit/recipe-edit.component';
import { RecipeDetailedResolver } from './_resolvers/recipe-detailed.resolver';
import { RecipeAddComponent } from './menus/menu-meals/menu-meals-recipes/recipe-add/recipe-add.component';
import { MenuSchedulerComponent } from './menus/menu-scheduler/menu-scheduler.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'menu', component: MenuMainComponent},
  {path: 'meals'   , component: MenuMealsComponent},
  {path: 'makelist', component: MenuMakeListComponent},
  {path: 'products', component: MenuMealsProductsComponent},
  {path: 'recipes', component: MenuMealsRecipesComponent},
  {path: 'recipes/:recipeId', component: RecipeEditComponent, resolve: {recipe: RecipeDetailedResolver}},
  {path: 'new-recipe', component: RecipeAddComponent},
  {path: 'scheduler', component: MenuSchedulerComponent},



];









@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
