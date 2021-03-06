import { Component, Input, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent implements OnInit {

  constructor() { }

  @Input() recipe: Recipe;
  
  ngOnInit(): void {
  }

}
