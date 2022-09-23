import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AddRecipeProductComponent } from 'src/app/modals/add-recipe-product/add-recipe-product.component';
import { Product } from 'src/app/_models/product';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeProduct } from 'src/app/_models/recipe-product';
import { Unit } from 'src/app/_models/unit';
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
  productsToAdd: Product[];
  isEdit: boolean = true;
  bsModalRef: BsModalRef;
  units: Unit[];


  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  };


  constructor(private toastr: ToastrService,private recipeService: RecipeService
    , private route: ActivatedRoute,private router: Router
    , private modalService: BsModalService
    , private mealService: MealsService) { 
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

    this.loadUnits();
  }

  updateRecipe(){
    this.recipeService.updateRecipe(this.recipe).subscribe(() =>{
      this.toastr.success('Recipe updated')
      this.editForm.form.markAsPristine();
      this.editForm.form.markAsUntouched();
      
      console.log(this.recipe);
    })
  }

  updateUnitName(uniName: string, i: number){
    this.recipe.recipeProducts[i].unitName = uniName;
    this.editForm.form.markAsDirty();
  }


  loadUnits(){
    this.mealService.getUnits().subscribe((response: any) => {
      
      this.units = response;
      
    })

  }

  openAddProductModal(){

    const config = {
      class: 'modal-dialog-centered',
      initialState:{}
    }
    this.bsModalRef = this.modalService.show(AddRecipeProductComponent, config);
    this.bsModalRef.content.addSelectedProducts.subscribe( res  => {
      
        
      const productsToAdd : Product[] = res
      //console.log(productsToAdd);
      
        //console.log(productsToAdd);
        const recipeProductsToAdd : RecipeProduct[] = [];
        var i = productsToAdd.length
        while (i--){
          // if its already on the list then no need to add it
          let indexFound = this.recipe
          .recipeProducts.findIndex(x => x.productId == productsToAdd[i].productId);
          
          if (indexFound > -1){
            productsToAdd.splice(i, 1);
          } 
          else{
            const recipeProductToAdd : RecipeProduct = {
              recipeProductId: 0,
              recipeId: this.recipe.recipeId,
              productName: productsToAdd[i].productName,
              productId: productsToAdd[i].productId,
              unitName: productsToAdd[i].unitName,
              quantity: 0
            }

            recipeProductsToAdd.push(recipeProductToAdd);


          }
        }
        
        if(recipeProductsToAdd.length > 0){

          recipeProductsToAdd.sort((a, b) => (a.productId < b.productId ? -1 : 1));
          this.recipeService.addRecipeProducts(recipeProductsToAdd).subscribe(() =>{
            this.reloadRecipe();
          })
        }
        else{
          this.toastr.success("Products already on the list");
          return;
        }
          
        //console.log(recipeProductsToAdd);
        

    })

  }

  reloadRecipe(){
    this.recipeService.getRecipe(this.recipe.recipeId).subscribe(recipe => {
      this.recipe = recipe;
    })
  }


  deleteRecipeProduct(recipeProductId: number){
    this.recipeService.deleteRecipeProduct(recipeProductId).subscribe(() =>{
      this.recipe.recipeProducts.splice(this.recipe.recipeProducts.findIndex(x => x.recipeProductId === recipeProductId),1);
      this.toastr.success("Product deleted");

      
    })
  }

}
