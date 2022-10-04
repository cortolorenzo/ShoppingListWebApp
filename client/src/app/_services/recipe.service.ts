import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseUrl = environment.apiUrl;
  recipes: Recipe[];

  constructor(private http: HttpClient, private modalService: BsModalService) { }

  getRecipes(){
    return this.http.get<Recipe[]>(this.baseUrl + 'recipes')
      .pipe(map(res => {
        this.recipes = res;
        return res;
      }));
    
  }

  getRecipe(recipeId: number, isReload: boolean){
    // console.log(this.recipes);
    if (this.recipes && !isReload){
      const recipe = this.recipes.find((recipe: Recipe) => recipe.recipeId == recipeId);
      if (recipe){
        return of(recipe);
      }
    }
    


    return this.http.get<Recipe>(this.baseUrl + 'recipes/' + recipeId);
  }

  setMainPhoto(photoId: number, recipeId: number){
    return this.http.put(this.baseUrl + 'recipes/' + recipeId + '/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number, recipeId: number){
    return this.http.delete(this.baseUrl + 'recipes/' + recipeId +'/delete-photo/' + photoId)

  }

  updateRecipe(recipe: Recipe){
    return this.http.put(this.baseUrl + 'recipes', recipe);
  }

  deleteRecipe(recipeId: number){
    return this.http.delete(this.baseUrl + 'recipes/' + recipeId);
  }

  deleteRecipeProduct(recipeId: number){
    return this.http.delete(this.baseUrl + 'recipes/del-recipe-product/' + recipeId);
  }

  addRecipe(recipe: any){
    return this.http.post<number>(this.baseUrl + 'recipes/', recipe);
  }


  addRecipeProducts(recipeProducts: any[]){
    return this.http.post<number>(this.baseUrl + 'recipes/add-recipe-products/', recipeProducts);
  }

}
