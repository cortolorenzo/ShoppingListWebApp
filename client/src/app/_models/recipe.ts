import { Photo } from "./photo";

export interface Recipe {
    recipeId: number;
    recipeName: string;
    recipeDescription: string;
    photoUrl?: string;
    photos: Photo[];
}