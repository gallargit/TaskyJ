import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let setContent = '';
    if (request.params)
      if (request.params.get('content'))
        setContent = request.params.get('content');
    request = request.clone({
      setHeaders: {
        authkeyadmin: `244a11741ddF6b30F6e4F6b1F6c2F6af1757F6bfeF6beF6c7F6fbF6fc26cf`,
        authkey: `ffffffffffffffffffffffffffffffff` //todo read this key from config
      }
    });
    // add authorization header with jwt token if available
    let currentUser = this.authenticationService.currentUserValue;
    if (currentUser) {
      request = request.clone({
        setHeaders: {
          authorization: `${currentUser.jwtToken}`,
          refreshToken: `${currentUser.refreshToken.token}`,
          contentxx: setContent,
        }
      });
    }
    return next.handle(request);
  }
}
