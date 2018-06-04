import { Injectable } from '@angular/core';
import { Response, Headers, RequestOptions } from '@angular/http';

import { UserRegistration } from '../models/user.registration.interface';
import { ConfigService } from '../utils/config.service';

import { BaseService } from "./base.service";

import '../../rxjs-operators';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable()

export class UserService extends BaseService {
  private loggedIn = false;

  constructor(private http: HttpClient, configService: ConfigService) {
    super(configService);
    this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    //this.authNavStatusSource.next(this.loggedIn);
  }

  register(userName: string, passwordHash: string, email: string): Observable<UserRegistration> {
    let body = JSON.stringify({ userName, passwordHash, email });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    //let options = new RequestOptions({ headers: headers });

    return this.http.post<UserRegistration>(this.baseUrl + "/account/api", body, { headers });
    //.pipe(map(res => res.json()));
    //.catch(this.handleError);
  }

  private async getToken() {
    let currentTime = ((new Date().getTime() * 10000) + 621355968000000000);

    let authTokenExpire = localStorage.getItem('auth_token_expire');

    if (currentTime > Number(authTokenExpire)) {
      let userName = localStorage.getItem('user_name');
      let refreshToken = localStorage.getItem('refresh_token');

      await this.refresh(userName, refreshToken);
    }

    let authToken = localStorage.getItem('auth_token');

    return `Bearer ${authToken}`;
  }

  async get(): Promise<Observable<any>> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    //headers.append('Authorization', `Bearer ${authToken}`);
    headers.append('Authorization', await this.getToken());

    return this.http.get<any>((this.baseUrl + "/test/get"), { headers });
    //.catch(this.handleError).toPromise();
  }

  async refresh(userName, refreshToken) {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    let grantType = 'refresh_token';

    return this.http
      .post(
      this.baseUrl + '/account/login',
      JSON.stringify({ userName, refreshToken, grantType }), { headers }
      )
      .subscribe((res: any) => {
        if (res.authenticated) {
          localStorage.setItem('auth_token', res.accessToken);
          localStorage.setItem('refresh_token', res.refreshToken);
          localStorage.setItem('auth_token_expire', res.accessTokenExpire);
          localStorage.setItem('refresh_token_expire', res.refreshTokenExpire);
          localStorage.setItem('user_name', res.userName)
          this.loggedIn = true;
        }

        return res.authenticated;
      });
  }

  login(userName, passwordHash) {
    let headers = new HttpHeaders().set('Content-Type', 'application/json');

    let grantType = 'password';

    var params = {
      userName: userName,
      passwordHash: passwordHash,
      grantType: grantType
    };

    return this.http
      .post(
      this.baseUrl + '/account/login',
      params, { headers } 
      )
      .map((res: any) => {
        if (res.authenticated) {
          localStorage.setItem('auth_token', res.accessToken);
          localStorage.setItem('refresh_token', res.refreshToken);
          localStorage.setItem('auth_token_expire', res.accessTokenExpire);
          localStorage.setItem('refresh_token_expire', res.refreshTokenExpire);
          localStorage.setItem('user_name', res.userName)
          this.loggedIn = true;
        }

        return res.authenticated;
      });
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('auth_token_expire');
    localStorage.removeItem('refresh_token_expire');
    localStorage.removeItem('user_name')

    this.loggedIn = false;

    //this.authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  //facebookLogin(accessToken: string) {
  //  let headers = new Headers();
  //  headers.append('Content-Type', 'application/json');
  //  let body = JSON.stringify({ accessToken });
  //  return this.http
  //    .post(
  //    this.baseUrl + '/externalauth/facebook', body, { headers })
  //    .map(res => res.json())
  //    .map(res => {
  //      localStorage.setItem('auth_token', res.auth_token);
  //      this.loggedIn = true;
  //      this.authNavStatusSource.next(true);
  //      return true;
  //    })
  //    .catch(this.handleError);
  //}
}

