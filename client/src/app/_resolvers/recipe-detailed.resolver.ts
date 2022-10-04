import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Recipe } from '../_models/recipe';
import { RecipeService } from '../_services/recipe.service';

@Injectable({
  providedIn: 'root'
})
export class RecipeDetailedResolver implements Resolve<Recipe> {

  constructor(private recipeService: RecipeService){}

  resolve(route: ActivatedRouteSnapshot): Observable<Recipe> {
    const recipeId = Number(route.paramMap.get('recipeId'));
    if (recipeId)
      return this.recipeService.getRecipe(Number(route.paramMap.get('recipeId')), false);
  }
}
