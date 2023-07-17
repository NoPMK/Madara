import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {

  @Input() form!: FormGroup;
  @Input() controlName!: string;
  @Input() errorProperty!: string;
  @Input() label!: string;
  @Input() inputErrorMessage!: string;
  @Input() type: string = "text";

  constructor() { }

  ngOnInit(): void {
  }

}
