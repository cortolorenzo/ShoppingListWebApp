import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';

@Component({
  selector: 'app-menu-scheduler',
  templateUrl: './menu-scheduler.component.html',
  styleUrls: ['./menu-scheduler.component.css'],
  providers: [
    { provide: CarouselConfig, 
      useValue: { interval: false, showIndicators: true } }]
      
})
export class MenuSchedulerComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
