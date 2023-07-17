import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { serverErrorHandler } from 'src/app/utils/server-error.handler';
import { AuthService } from 'src/app/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NgToastService } from 'ng-angular-popup';
import { NotificationService } from '../toastr-notifications/toastr-notification.service';

declare const FB: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loginForm!: FormGroup;
  switch: boolean = false;
  btnScale: boolean = false;
  registerSubmitted: boolean = false;
  loginSubmitted: boolean = false;
  currentUserisValid: boolean = false;
  email: any;
  emailAgain:any;



  constructor(private userService: UserService, private router: Router, private authService: AuthService, private _ngZone: NgZone, private toast: NotificationService) { }
  ngOnInit(): void {
    this.registerFormInit();
    this.loginFormInit();
  }

  register() {
    if(this.registerForm.valid && this.getmail()){
      this.registerSubmitted = true;
      const newUser = this.registerForm.value;
      this.userService.registerUser(newUser).subscribe({
        next: (res) => {
          this.registerForm.reset();
          this.toast.success("Your registration was successful!")
          this.router.navigate(['register']);
  
        },
        error: (err) => {
          console.log(err)
          if (err instanceof HttpErrorResponse && err.status === 400 && err.error.errors.UserName) {
            this.toast.error("Username is too short!")
          } 
          else if (err instanceof HttpErrorResponse && err.status === 400 && err.error.errors.Password) 
          {
            this.toast.error("Invalid Password")
          }
           else if (this.getmail() === false) 
          {
            if(err instanceof HttpErrorResponse && err.status === 400 && err.error.errors.Email)
            this.toast.error("Invalid Email!")
          }
        }
      })
    }
    else{
      this.toast.error("Your email addresses are not macth!")
      this.registerForm.reset();  
    }  
  }

  login() {
    this.loginSubmitted = true;
    const loginData = this.loginForm.value;

    this.authService.login(loginData).subscribe({
      next: () => {
        this.toast.success("You Logged in Successfully!")
        setTimeout(() => { this.router.navigate(['home']) }, 1000)
      },
      error: (err) => {
        console.log(err);
        if (err instanceof HttpErrorResponse && err.status === 400 && err.error) {
          this.openLoginErrorMessage();
        }
        if (err instanceof HttpErrorResponse && err.status === 400 && err.error) {
          this.openLoginErrorMessage();
        }
      }
    })
  }


  registerFormInit() {
    this.registerForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      // email: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,63}$',)]),
      email: new FormControl('',[
        Validators.required,
        Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      // emailAgain: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,63}$',)]),
      emailAgain: new FormControl('',[
        Validators.required,
        Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      password: new FormControl('', [Validators.required, Validators.minLength(8), Validators.pattern(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?([^\w\s]|[_])).{8,}$/)])
    })
  }

  loginFormInit() {
    this.loginForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)])
    })
  }

  panelClick() {
    this.switch = !this.switch;
    this.scaleButton();
  }

  scaleButton() {
    window.requestAnimationFrame(() => {
      this.btnScale = !this.btnScale;
    })
  }

  async facebookLogin() {
    FB.login(async (result: any) => {
      await this.userService.LoginWithFacebook(result.authResponse.accessToken).subscribe(
        (x: any) => {
          this._ngZone.run(() => {
            this.router.navigate(['/logout']);
          })
        },
        (error: any) => {
          console.log(error);
        }
      );
    }, { scope: 'email' });

  }

  openSuccesRegister() {
    this.toast.success("Your registration is successful!")
  }
  openSuccesLogin() {
    this.toast.success("You logged in Successfully!")
  }
  openLoginErrorMessage() {
    this.toast.error("Provided login credentials are invalid!")
  }


  getmail(){
    if(this.email === this.emailAgain)
    {
      return true;
    }
    return false;
  }
}

