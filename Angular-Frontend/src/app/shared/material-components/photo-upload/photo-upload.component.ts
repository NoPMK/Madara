import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {  MatIconRegistry } from '@angular/material/icon';


@Component({

  selector: 'file-upload', 

  templateUrl: "photo-upload.component.html",

  styleUrls: ["photo-upload.component.css"]

})

export class PhotoUploadComponent {
    

    fileName = '';

    constructor(private http: HttpClient) {
       
    }

    onFileSelected(event:any) {

        const file:File = event.target.files[0];

        if (file) {

            this.fileName = file.name;

            const formData = new FormData();

            formData.append("thumbnail", file);

            const upload$ = this.http.post("/api/thumbnail-upload", formData);


            upload$.subscribe();

        }

    }

}
