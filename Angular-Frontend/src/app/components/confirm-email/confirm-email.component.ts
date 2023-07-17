import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {

  emailConfirmed: boolean = false;
  urlParams: any = {};

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.urlParams.token = params['token'];
      this.urlParams.userid = params['userid'];

      if (this.urlParams.token && this.urlParams.userid)
      {
        this.confirmEmail();
      }
    })
  }

  confirmEmail(){
    this.userService.confirmEmail(this.urlParams).subscribe({
      next: () => {
        this.emailConfirmed = true;
      }
    })
  }

  backToLogin() {
    this.router.navigate(['register']);
  }
}
