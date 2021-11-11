import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { GeneralService } from '../services/general.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private MAIN_URL: string = 'http://localhost:49002/api/taskyj/'
  //private MAIN_URL: string = 'https://localhost:44332/api/taskyj/'
  private MASTER_URL: string = this.MAIN_URL + 'GetMaster';

  public testtasks: any = '(retrieving...)';
  public testtasks2: any = '(retrieving...)';

  constructor(
        private generalService: GeneralService,
        private http: HttpClient
    ) {
/*
      this.http.get(this.MASTER_URL, { responseType: 'text'}).subscribe(
            results => this.testtasks = results,
            error => this.testtasks = "Error: " + error);*/
      this.generalService.Status().subscribe(
            results => {
              this.testtasks = results;
            },
            error => this.testtasks = "Dead (CORS?) " + error);

        this.generalService.GetMaster().subscribe(
            results => {
              var tmp = '';
              this.testtasks2 = '';
              results.forEach(element => {
                    for (var key in element){
                        tmp += key + ': '+ element[key] + ',';
                    }
                    this.testtasks2+='[' + tmp + '],<br/><br/>';
                });},
            error => this.testtasks2 = "Error: " + error);
    }
}
