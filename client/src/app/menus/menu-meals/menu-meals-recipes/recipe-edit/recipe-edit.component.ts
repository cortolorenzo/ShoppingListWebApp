import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-recipe-edit',
  templateUrl: './recipe-edit.component.html',
  styleUrls: ['./recipe-edit.component.css']
})
export class RecipeEditComponent implements OnInit {

  @ViewChild('editForm') editForm:NgForm;
  recipe: Recipe;
  isEdit: boolean = true;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  };


  constructor(private toastr: ToastrService,private recipeService: RecipeService, private route: ActivatedRoute,private router: Router) { 
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit(): void {


    this.route.queryParams.subscribe(params => {
      if (params.isEdit)
        this.isEdit = params.isEdit;
    })

    if (this.isEdit)
    this.route.data.subscribe(data => {
      this.recipe = data.recipe;
      console.log(this.recipe);
    })
   

   

  }

  updateRecipe(){
    this.recipeService.updateRecipe(this.recipe).subscribe(() =>{
      this.toastr.success('Recipe updated')
      this.editForm.reset(this.recipe);
    })
  }

}
