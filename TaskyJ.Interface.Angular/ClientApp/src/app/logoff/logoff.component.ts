import { BrowserModule } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';
import { GeneralService } from '../services/general.service';
import { DBSessionJ } from '../models/DBSessionJ';

@Component({
  selector: 'app-logoff',
  templateUrl: './logoff.component.html',
  styleUrls: ['./logoff.component.css']
})
export class LogoffComponent implements OnInit {
  public logoffstatus: string = '--';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private generalService: GeneralService,
    private authenticationService: AuthenticationService) {
  }

  ngOnInit(): void {
    this.logoffstatus = 'working';

    this.authenticationService.logoff().subscribe(
      result => this.logoffstatus=result
    );

    //this.router.navigate(['/home']);
  }
}
