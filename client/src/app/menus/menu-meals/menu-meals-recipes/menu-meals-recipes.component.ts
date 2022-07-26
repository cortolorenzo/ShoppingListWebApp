import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-menu-meals-recipes',
  templateUrl: './menu-meals-recipes.component.html',
  styleUrls: ['./menu-meals-recipes.component.css']
})
export class MenuMealsRecipesComponent implements OnInit {

  recipes: Recipe[];

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.loadRecipes();
  }


  loadRecipes(){
    this.recipeService.getRecipes().subscribe((response: any) => {
      this.recipes = response;
      console.log(this.recipes );

    })

  }





}
