import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/user-model';
import { AuthService } from 'src/app/services/auth.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  user?: UserModel | null;
  isOpen?: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    public loaderService: LoaderService
  ) {}

  ngOnInit(): void {
      this.authService.user.subscribe({
        next: (user) => {
          this.user = user;
        }
      })
      this.refreshUser();
    }
  
  

  refreshUser() {
    if (localStorage.getItem('accessToken') != undefined) {
      this.authService.getMe().subscribe();
    }
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['register']);
  }

  extendMenu() {
    this.isOpen = !this.isOpen;
  }
}
