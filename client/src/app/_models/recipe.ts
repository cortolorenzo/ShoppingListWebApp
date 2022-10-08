import { Photo } from "./photo";
import { RecipeProduct } from "./recipeProduct";

export interface Recipe {
    recipeId: number;
    recipeName: string;
    recipeDescription: string;
    photoUrl?: string;
    photos: Photo[];
    recipeProducts: RecipeProduct[];
}