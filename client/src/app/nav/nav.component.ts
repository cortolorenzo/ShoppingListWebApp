import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}

  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
     if (this.accountService.isAuthenticated())
      this.router.navigateByUrl('/menu');
  }

  login(){

    this.accountService.login(this.model).subscribe(response => {


      this.router.navigateByUrl('/menu');
     // console.log(response);
     
    
    })

  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
 
  }

}
