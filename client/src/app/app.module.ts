import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinner, NgxSpinnerModule } from 'ngx-spinner';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuMainComponent } from './menus/menu-main/menu-main.component';
import { MenuMealsComponent } from './menus/menu-meals/menu-meals.component';
import { MenuMakeListComponent } from './menus/menu-make-list/menu-make-list.component';
import { MenuMealsProductsComponent } from './menus/menu-meals/menu-meals-products/menu-meals-products.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { EditProductModalComponent } from './modals/edit-product/edit-product-modal/edit-product-modal.component';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { AddProductModalComponent } from './modals/edit-product/add-product-modal/add-product-modal.component';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { EditUnitsComponent } from './modals/edit-product/edit-units/edit-units.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { SharedModule } from './shared/shared.module';
import { MenuMealsRecipesComponent } from './menus/menu-meals/menu-meals-recipes/menu-meals-recipes.component';
import { RecipeCardComponent } from './menus/menu-meals/menu-meals-recipes/recipe-card/recipe-card.component';
import { RecipeEditComponent } from './menus/menu-meals/menu-meals-recipes/recipe-edit/recipe-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './common/photo-editor/photo-editor.component';
import { RecipeAddComponent } from './menus/menu-meals/menu-meals-recipes/recipe-add/recipe-add.component';
import { AddRecipeProductComponent } from './modals/add-recipe-product/add-recipe-product.component';
import { MenuSchedulerComponent } from './menus/menu-scheduler/menu-scheduler.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { SchedulerAddRecipeComponent } from './modals/scheduler-add-recipe/scheduler-add-recipe.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { AccountService } from './_services/account.service';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';





@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MenuMainComponent,
    MenuMealsComponent,
    MenuMakeListComponent,
    MenuMealsProductsComponent,
    EditProductModalComponent,
    AddProductModalComponent,
    ConfirmDialogComponent,
    EditUnitsComponent,
    MenuMealsRecipesComponent,
    RecipeCardComponent,
    RecipeEditComponent,
    PhotoEditorComponent,
    RecipeAddComponent,
    AddRecipeProductComponent,
    MenuSchedulerComponent,
    SchedulerAddRecipeComponent,
    HomeComponent,
    RegisterComponent,
    TextInputComponent,


    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ModalModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgxSpinnerModule,
    CarouselModule.forRoot(),
    
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass:ErrorInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass:LoadingInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi:true},
    [BsModalService]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
