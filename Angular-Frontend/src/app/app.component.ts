import { UserModel } from 'src/app/models/user-model';
import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Angular-Frontend';

  // constructor(private titleService: Title){
  //   this.titleService.setTitle($localize`${this.title}`);
  // }
}
