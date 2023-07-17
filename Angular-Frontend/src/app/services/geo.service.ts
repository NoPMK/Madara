import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as L from 'leaflet';

@Injectable({
  providedIn: 'root'
})
export class GeoService {
latitude: any = '';
longitude: any = '';

  constructor(private http: HttpClient) { }


  //TODO: Rájönni, hogy először miért undefined a két koordináta és utána miért jó.

  geocodeAddress(address: string) {
    const url = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(address)}&format=json`;
    this.http.get(url)
      .subscribe((response: any) => {
        if (response.length > 0) {
          this.latitude = response[0].lat;
          this.longitude = response[0].lon;         
          console.log(`Latitude: ${this.latitude}, Longitude: ${this.longitude}`);
        } else {
          console.log('Address not found.');         
        }
      }, (error) => {
        console.log('Error occurred during geocoding:', error);
      });
  }


  getCurrentPosition() {
    if (!navigator.geolocation) alert('This location is not supported!');
    navigator.geolocation.getCurrentPosition((pos) => {
      const userLatLng: any = [pos.coords.latitude, pos.coords.longitude];     
      const latLong: any = [this.latitude, this.longitude];
      console.log(
        `lat1: ${this.latitude}, lon1: ${this.longitude}`
      );
      let map = L.map('map').setView(latLong, 12);
      L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution:
          '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      }).addTo(map);


      let marker = L.marker(latLong).addTo(map);
      let userMarker = L.marker(userLatLng).addTo(map);
      const line = L.polyline([marker.getLatLng(), userMarker.getLatLng()], { color: 'red' }).addTo(map);
      const lineLengthInMeters = marker.getLatLng().distanceTo(userMarker.getLatLng());
      const formattedLineLength = lineLengthInMeters.toFixed(0);
      const parsedLineLength = Number(formattedLineLength);
      let circle = L.circle(latLong, 500).addTo(map);


      userMarker.bindPopup('You are here!').openPopup();
      marker.bindPopup('Property!').openPopup();
      line.bindPopup(`${parsedLineLength / 1000} Kilometers to Property`).openPopup();
    });
    this.watchPosition();
  }


  watchPosition() {
    let destinationLat = 0;
    let destinationLong = 0;
    let id = navigator.geolocation.watchPosition(
      (position) => {
        console.log(
          `latWatch: ${position.coords.latitude}, lonWatch: ${position.coords.longitude}`
        );
        if (
          position.coords.latitude == destinationLat &&
          position.coords.longitude == destinationLong
        ) {
          navigator.geolocation.clearWatch(id);
        }
      },
      (err) => {
        console.log(err);
      },
      {       
        timeout: 5000,
        maximumAge: 0,
      }
    );
  }

  getCurrentUserPosition(){
    if (!navigator.geolocation) alert('This location is not supported!');
    navigator.geolocation.getCurrentPosition((pos) => {
      const latLong: any = [pos.coords.latitude, pos.coords.longitude];
      console.log(
        `lat1: ${this.latitude}, lon1: ${this.longitude}`
      );
      let map = L.map('map').setView(latLong, 13);
      L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution:
          '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      }).addTo(map);

      let marker = L.marker(latLong).addTo(map);

      marker.bindPopup('Property!').openPopup();
    });
    this.watchPosition();
  }
}
