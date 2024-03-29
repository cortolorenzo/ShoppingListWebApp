import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1)
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient) {
  //  console.log("Accoutn service created") 
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user){
          this.setCurrentUser(user);
          
        }
        return user;
      })
    )
  }

  login(model: any){

    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) =>{
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    
  }

  
  setCurrentUser(user: User){
    
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);

    localStorage.setItem('user',JSON.stringify(user))
    this.currentUserSource.next(user);
  }

  getDecodedToken(token: string){
    return JSON.parse(atob(token.split('.')[1]))
  }


  isAuthenticated() {
    let user: User;
    

    try {
      user = JSON.parse(localStorage.getItem('user'));
      const { exp } = this.getDecodedToken(user.token);
      const d = new Date(0);
      d.setUTCSeconds(exp);
      //console.log(d);


      //console.log(exp);
      if (exp < (new Date().getTime() + 1) / 1000) {
        this.logout();

        return false;
      }
    } catch (err) {
      this.logout();
      return false;
    }
    return true;
  }

}
