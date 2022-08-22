import { Component,EventEmitter , Input, OnInit, Output } from '@angular/core';

import { Recipe } from 'src/app/_models/recipe';


@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent implements OnInit {

  constructor() { }

  @Input() recipe: Recipe;
  @Output() deleteRecipeEvent = new EventEmitter<Recipe>();
  
  ngOnInit(): void {
  }

  deleteRecipe(recipe: Recipe){
    console.log("Im here");
    
    this.deleteRecipeEvent.emit(recipe);
  }  

 

}
