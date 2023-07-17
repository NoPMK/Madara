import { Component,ElementRef, OnInit } from '@angular/core';
import  VanillaTilt  from "vanilla-tilt";

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit{

  constructor(private el: ElementRef) {
  }

  ngOnInit() {
    VanillaTilt.init(
      this.el.nativeElement.querySelectorAll(".card"), { max: 20, speed: 400, scale: 1.05 }
    );}

}
