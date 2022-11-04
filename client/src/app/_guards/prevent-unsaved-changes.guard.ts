import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipeEditComponent } from '../menus/menu-meals/menu-meals-recipes/recipe-edit/recipe-edit.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {


 constructor(private confirmService: ConfirmService){}

  canDeactivate(component: RecipeEditComponent): Observable<boolean> | boolean{
    if (component.editForm.dirty) {
      return this.confirmService.confirm("Changes unsaved","Are you sure you want to continue?");
      //return confirm('Are you sure you want to continue? Any insaved changes will be lost')
    }
    return true;
  }
  
}
  

