import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordForm!: FormGroup;
  email: string = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.formInit();
  }


  formInit(){
    this.forgotPasswordForm = new FormGroup({
      email:new FormControl('',[Validators.required,Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,63}$',)]),
      password: new FormControl('',[Validators.required, Validators.minLength(8),Validators.pattern(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?([^\w\s]|[_])).{8,}$/)]),
      passwordAgain: new FormControl('',[Validators.required, Validators.minLength(8),Validators.pattern(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?([^\w\s]|[_])).{8,}$/)]),
    })
  }

  forgotPassword(){
    
  }

}
