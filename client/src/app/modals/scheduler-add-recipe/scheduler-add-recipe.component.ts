import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-scheduler-add-recipe',
  templateUrl: './scheduler-add-recipe.component.html',
  styleUrls: ['./scheduler-add-recipe.component.css']
})
export class SchedulerAddRecipeComponent implements OnInit {

  @Input() addSelectedRecipes = new EventEmitter();
  recipes: any[];

  constructor(public bsModalRef: BsModalRef, private recipeservice: RecipeService ) { }

  ngOnInit(): void {
    this.getRecipes();
  }

  addRecipes(){
    const recipesToAdd = this.recipes.filter(el => el.checked === true);
 //   console.log(recipesToAdd)
    this.addSelectedRecipes.emit(recipesToAdd);
    this.bsModalRef.hide();

  }

  getRecipes(){
    this.recipeservice.getRecipes().subscribe((res: any) => {
      this.recipes = res;
      
    })
  }



}
