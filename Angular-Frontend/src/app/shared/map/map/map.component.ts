import { AfterContentInit, Component, Input, OnInit } from '@angular/core';
import { GeoService } from 'src/app/services/geo.service';

declare const L: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})
export class MapComponent implements OnInit, AfterContentInit {

@Input() address!: string;

  constructor(private geo: GeoService) {}

  ngOnInit(): void { 
   console.log(this.address)    
  }

  ngAfterContentInit(): void {
    this.geo.geocodeAddress(this.address);

    setTimeout(() => {
      this.geo.getCurrentPosition();
    }, 900);
  }
}
