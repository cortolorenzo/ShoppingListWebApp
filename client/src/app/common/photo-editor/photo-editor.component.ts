import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from 'src/app/_models/photo';
import { Recipe } from 'src/app/_models/recipe';
import { RecipeService } from 'src/app/_services/recipe.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() recipe:Recipe;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  //user: User | null;

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  setMainPhoto(photo: Photo){
    this.recipeService.setMainPhoto(photo.id, this.recipe.recipeId).subscribe(() => {
      this.recipe!.photoUrl = photo.url;
      //this.accountService.setCurrentUser(this.user!);
      this.recipe.photoUrl = photo.url;
      this.recipe.photos.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      })
    })
  }


  deletePhoto(photoId: number){
    this.recipeService.deletePhoto(photoId,this.recipe.recipeId).subscribe( () => {
      this.recipe.photos = this.recipe.photos.filter(x => x.id !== photoId);
    })
  }


  initializeUploader(){
    this.uploader = new FileUploader({
      url: this.baseUrl + 'recipes/add-photo/'+ this.recipe.recipeId,
      authToken: '' ,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024

    });

    this.uploader.onAfterAddingAll = (file) => {
      file.withCredentials = true;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: Photo = JSON.parse(response);
        this.recipe.photos.push(photo);
          if (photo.isMain) {
            this.recipe.photoUrl = photo.url;
            
            //this.accountService.setCurrentUser(this.user)
          }

      }
    }

  }

}
