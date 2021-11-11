import { BrowserModule } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';
import { GeneralService } from '../services/general.service';
import { DBSessionJ } from '../models/DBSessionJ';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  user: any = null; //todo
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  serverstatus: string;
  servererror: string;
  error: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private generalService: GeneralService,
    private authenticationService: AuthenticationService
  ) {
    if (!this.user) {
        this.user = this.authenticationService.currentUserValue;
    }
    if (this.authenticationService.currentUserValue || this.user) {
      this.router.navigate(['/home']);
    }

    this.serverstatus = "(checking)";
    this.generalService.Status().subscribe(
      status => this.serverstatus = status,
      error => this.serverstatus = "Error: " + error);
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.authenticationService.login(this.f.username.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        user => {
          this.user = user;
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.servererror = error;
          this.loading = false;
        });
  }
}
