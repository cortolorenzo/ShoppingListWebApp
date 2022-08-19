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
    NgxSpinnerModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass:ErrorInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass:LoadingInterceptor, multi:true},
    [BsModalService]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
