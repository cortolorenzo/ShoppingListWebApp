import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private modalService: BsModalService) { }

  getRecipes(){
    return this.http.get<Recipe[]>(this.baseUrl + 'recipes');
    
  }

}
