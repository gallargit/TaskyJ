import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DBSessionJ } from '../models/DBSessionJ';
import { GeneralService } from './general.service'

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  protected currentUserSubject: BehaviorSubject<DBSessionJ>;
  public currentUser: Observable<DBSessionJ>;

  constructor(private http: HttpClient) {
    if (localStorage.getItem('currentUserDate')) {
      if (localStorage.getItem('currentUserDate') != this.Today()) {
        localStorage.removeItem('currentUser');
        localStorage.removeItem('currentUserDate');
      }
    }
    this.currentUserSubject = new BehaviorSubject<DBSessionJ>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  private Today(): string {
    var d = new Date();
    var month = '' + (d.getMonth() + 1);
    var day = '' + d.getDate();
    var year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('');
  }

  public get currentUserValue(): DBSessionJ {
    return this.currentUserSubject.value;
  }

  public login(username: string, password: string) {
    //ORIG this.http.post<any>(GeneralService.LOGIN_URL, { UserName: username, Password: password })
    return this.http.post<DBSessionJ>(GeneralService.LOGIN_URL, { UserName: username, Password: password })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.jwtToken) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUserDate', this.Today());
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }
        return user;
      }));
  }

  public logoff() {
    var token = '';
    if (this.currentUserValue !== null)
      if (this.currentUserValue.jwtToken !== null)
        token = this.currentUserValue.jwtToken;
    localStorage.setItem('currentUserDate', null);
    localStorage.setItem('currentUser', null);

    if (token !== '') {
      return this.http.post<any>(GeneralService.LOGOFF_URL, { token: token })
        .pipe(map(result => {
          return result;
        }));
    }
  }
}
