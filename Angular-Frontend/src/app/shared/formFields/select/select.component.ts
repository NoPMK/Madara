import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { EnumModel } from 'src/app/models/types/enum-model';

export interface OptionModel extends EnumModel  {
 
}
@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css']
})
export default class SelectComponent implements OnInit {

  @Input() form!: FormGroup;
  @Input() controlName!: string;
  @Input() errorProperty!: string;
  @Input() label!: string;
  @Input() inputErrorMessage!: string;
  @Input() options: OptionModel[] = [];


  constructor() { }

  ngOnInit(): void {
  }

}