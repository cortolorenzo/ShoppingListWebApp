import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-menu-scheduler',
  templateUrl: './menu-scheduler.component.html',
  styleUrls: ['./menu-scheduler.component.css'],
  providers: [
    { provide: CarouselConfig, 
      useValue: { interval: false, showIndicators: true } }]
      
})
export class MenuSchedulerComponent implements OnInit {

  constructor(private cdRef:ChangeDetectorRef) { }

  ngAfterViewChecked()
  {
    this.cdRef.detectChanges();
  }

  private _activeSlideIndex: number;

  get activeSlideIndex(): number {
    return this._activeSlideIndex;
  };

  set activeSlideIndex(newIndex: number) {
    if(newIndex)
    if(this._activeSlideIndex !== newIndex) {
      console.log('Active slider index would be changed!' + newIndex);
      // here's the place for your "slide.bs.carousel" logic
    }
    this._activeSlideIndex = newIndex;
  };


  ngOnInit(): void {
  }

}
