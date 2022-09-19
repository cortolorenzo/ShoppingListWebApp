import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AddRecipeProductComponent } from 'src/app/modals/add-recipe-product/add-recipe-product.component';
import { Product } from 'src/app/_models/product';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeProduct } from 'src/app/_models/recipe-product';
import { MealsService } from 'src/app/_services/meals.service';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-recipe-edit',
  templateUrl: './recipe-edit.component.html',
  styleUrls: ['./recipe-edit.component.css']
})
export class RecipeEditComponent implements OnInit {

  @ViewChild('editForm') editForm:NgForm;
  recipe: Recipe;
  products: Product[];
  isEdit: boolean = true;
  bsModalRef: BsModalRef;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  };


  constructor(private toastr: ToastrService,private recipeService: RecipeService
    , private route: ActivatedRoute,private router: Router
    , private modalService: BsModalService) { 
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
    })
  }

  updateRecipe(){
    this.recipeService.updateRecipe(this.recipe).subscribe(() =>{
      this.toastr.success('Recipe updated')
      this.editForm.form.markAsPristine();
      this.editForm.form.markAsUntouched();
      
      console.log(this.recipe);
    })
  }



  openAddProductModal(){

   const config = {
    class: 'modal-dialog-centered',
    initialState:{}
  }
  this.bsModalRef = this.modalService.show(AddRecipeProductComponent, config);
  this.bsModalRef.content.addSelectedProducts.subscribe( productsToAdd => {
      console.log(productsToAdd);
  })

  }

  deleteProduct(recipeProductId: number){

  }

}
