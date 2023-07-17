import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  public searchInput!: string;
  
  constructor() { }
  
  ngOnInit(): void {
  }
  
  @Output()
  searchTextChanged: EventEmitter<string> = new EventEmitter<string>();

  onsearchTextChanged(){
    this.searchTextChanged.emit(this.searchInput);
  }
  
}
