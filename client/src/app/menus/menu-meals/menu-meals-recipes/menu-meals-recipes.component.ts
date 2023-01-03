import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Recipe } from 'src/app/_models/recipe';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-menu-meals-recipes',
  templateUrl: './menu-meals-recipes.component.html',
  styleUrls: ['./menu-meals-recipes.component.css']
})
export class MenuMealsRecipesComponent implements OnInit {

  recipes: Recipe[];

  constructor(private recipeService: RecipeService
    , private toastr: ToastrService
    , private confirmService: ConfirmService) { }

  ngOnInit(): void {
    this.loadRecipes();
  }


  loadRecipes(){
    this.recipeService.getRecipes().subscribe((response: any) => {
      this.recipes = response;
     // console.log(this.recipes );

    })

  }

  deleteRecipe(recipe: Recipe){
    this.confirmService.confirm('Recipe delete', 'Are you sure you want to delete: ' + recipe.recipeName).subscribe(result => {
      if(result) {
        this.recipeService.deleteRecipe(recipe.recipeId).subscribe(() => {
          this.recipes.splice(this.recipes.findIndex(x => x.recipeId === recipe.recipeId),1);
          this.toastr.success("Recipe deleted!");
        });

      }
    })
  }





}
