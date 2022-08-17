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

  getRecipe(recipeId: number){
    console.log(this.recipes);
    if (this.recipes){
      const recipe = this.recipes.find((recipe: Recipe) => recipe.recipeId == recipeId);
      if (recipe){
        return of(recipe);
      }
    }
    


    return this.http.get<Recipe>(this.baseUrl + 'recipes/' + recipeId);
  }


}
