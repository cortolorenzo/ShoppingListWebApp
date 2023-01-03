import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrls: ['./recipe-add.component.css']
})
export class RecipeAddComponent implements OnInit {

  @ViewChild('editForm') editForm:NgForm;
  recipe: any = {};
  
  validationErrors: string[] = [];

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  };


  constructor(private toastr: ToastrService,private recipeService: RecipeService,private router: Router) { 
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit(): void {


    
  }

  addRecipe(){
   // console.log(this.recipe)
    
    this.recipeService.addRecipe(this.recipe).subscribe(recipeId =>{
      this.toastr.success('Recipe added');
     // console.log(recipeId);
      this.router.navigateByUrl('recipes/' + recipeId)
      
    }, error => {
      this.validationErrors = error;
    })
  }


}
