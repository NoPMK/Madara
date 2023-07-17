import { AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import * as Parallax from 'parallax-js';

@Component({
  selector: 'app-background',
  templateUrl: './background.component.html',
  styleUrls: ['./background.component.css'],
})
export class BackgroundComponent implements OnInit, AfterViewInit {

@ViewChild('bldg3') bldg3!: ElementRef<HTMLImageElement>
@ViewChild('bldg2') bldg2!: ElementRef<HTMLImageElement>
@ViewChild('bldg1') bldg1!: ElementRef<HTMLImageElement>
@ViewChild('text') text!: ElementRef<HTMLImageElement>
@ViewChild('fog') fog!: ElementRef<HTMLImageElement>
@ViewChild('plane') plane!: ElementRef<HTMLImageElement>
@ViewChild('bg') bg!: ElementRef<HTMLImageElement>

  constructor() {}

  ngOnInit(): void {
    var city = document.getElementById('easyPara')!;
    var parallaxInstance = new Parallax(city);
  }

  ngAfterViewInit(): void {
    
      this.bldg3.nativeElement.style.removeProperty('left');
      this.bldg3.nativeElement.style.removeProperty('top');

      this.bldg2.nativeElement.style.removeProperty('left');
      this.bldg2.nativeElement.style.removeProperty('top');

      this.bldg1.nativeElement.style.removeProperty('left');
      this.bldg1.nativeElement.style.removeProperty('top');

      this.text.nativeElement.style.removeProperty('left');
      this.text.nativeElement.style.removeProperty('top');

      this.fog.nativeElement.style.removeProperty('left');
      this.fog.nativeElement.style.removeProperty('top');

      this.plane.nativeElement.style.removeProperty('left');
      this.plane.nativeElement.style.removeProperty('top');

      this.bg.nativeElement.style.removeProperty('left');
      this.bg.nativeElement.style.removeProperty('top');
  }
}