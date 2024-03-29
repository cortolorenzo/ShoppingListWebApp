import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { NgControl, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements ControlValueAccessor {

  @Input() label: string;
  @Input() type = 'text';
  constructor(@Self() @Optional() public ngControl: NgControl) {
    ngControl.valueAccessor = this;
   }

  writeValue(obj: any): void {
  }
 
  registerOnChange(fn: any): void {
  }
 
  registerOnTouched(fn: any): void {
  }
  

}
