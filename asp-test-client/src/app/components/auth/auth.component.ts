import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private as: AuthService,
    private router: Router
  ) { }

  form!: FormGroup;

  ngOnInit(): void {
  }

  myForm : FormGroup = new FormGroup({
    "login": new FormControl(),
    "password": new FormControl()
  });

  logIn() {
    this.as.logIn(this.myForm.controls['login'].value, this.myForm.controls['password'].value)
    .subscribe(() => 
    this.router.navigate(['/products'])
    , error => {
      
    });
  };
}
